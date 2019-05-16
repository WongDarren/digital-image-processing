// CNN_cpp.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include <iostream>
#include <fstream>
#include <string>
#include <limits>
using namespace std;

int reverseInt(int);
double sigmoid(double);

// Constants
const int TRAINING = 60000;
const int TESTING = 10000;
const int IMG_ROWS = 28;
const int IMG_COLS = 28;
const int FRAMES = 6;
const int WINDOWSIZE = 5;
const int HIDDEN = 45;
const int OUT = 10;
const int F1RES = 24;
const int F2RES = 12;
const int EPOCHS = 100;
const int MINIBATCHES = 1;
const double LEARNRATE = 0.0001;

// Image Data
double trainImagePixels[TRAINING][IMG_ROWS][IMG_COLS];
double trainImageLabels[TRAINING];
double testImagePixels[TESTING][IMG_ROWS][IMG_COLS];
double testImageLabels[TESTING];

int main()
{
	// Image Data
	double trainImagePixels[TRAINING][IMG_ROWS][IMG_COLS];
	double trainImageLabels[TRAINING];
	double testImagePixels[TESTING][IMG_ROWS][IMG_COLS];
	double testImageLabels[TESTING];

	// Trainable Paramaters
	double w[FRAMES][WINDOWSIZE][WINDOWSIZE];   	// weights			(w)
	double b0[FRAMES];                           	// biases			(b0)
	double b1[HIDDEN];                         		// biases			(b1)
	double u[HIDDEN][FRAMES][F2RES][F2RES]; 		// weights			(u)
	double b2[OUT];                            		// biases			(b2)
	double v[OUT][HIDDEN];                    		// weights			(v)

	// Trainable Paramater Grads
	double w_grad[FRAMES][WINDOWSIZE][WINDOWSIZE];   	// new weights				(w_grad)
	double b0_grad[FRAMES];                           	// new biases				(b0_grad)
	double b1_grad[HIDDEN];                         	// new biases				(b1_grad)
	double u_grad[HIDDEN][FRAMES][F2RES][F2RES]; 		// new weights				(u_grad)
	double b2_grad[OUT];                            	// new biases				(b2_grad)
	double v_grad[OUT][HIDDEN];                    		// new weights				(v_grad)

	// Feed Forward Data (S | O)
	double imgInput[IMG_ROWS][IMG_COLS];           		// image for input
	double s0[FRAMES][F1RES][F1RES];					// convolutional layer S value
	double cl_y[FRAMES][F1RES][F1RES];					// convolutional layer Y value
	double cl_y_sum[FRAMES][F1RES][F1RES];				// convolutional layer total sum
	double pixel_track[FRAMES][F2RES][F2RES];			// pooling layer pixel tracking (0-3) ((2*j) + i)
	double pool_layer[FRAMES][F2RES][F2RES];			// pooling layer values
	double s1[HIDDEN];									// fully connected layer 1 S values
	double fl_y0[HIDDEN];								// fully connected layer 1 Y values
	double s2[OUT];										// fully connected layer 2 S values
	double fl_y1[OUT];									// fully connected layer 2 Y values
	double z[OUT];										// Z values of forward propagation
	double answer[OUT];									// answer array for training image

	// Back Propagation
	double error[OUT];						// error for training example
	double z_error[OUT];           			// Temp for z Back prop
	double hidden_sum_error[HIDDEN];		// Temp for HIDDEN Back prop
	double first_layer_sum_error[FRAMES];	// Temp for First layer back prop
	double cl_error[FRAMES][F2RES][F2RES];	// Temp for CL back prop

	cout << "Reading Training Images and Labels!" << endl;
	// +--------------------------------------------------+
	// |         Read Training Images and Labels          |
	// +--------------------------------------------------+
	ifstream trainingImages("data/train-images-idx3-ubyte");
	ifstream trainingLabels("data/train-labels-idx1-ubyte");
	if (trainingImages.is_open())
	{
		int magicNum = 0;
		int trainSize = 0;
		int n_rows = 0;
		int n_cols = 0;

		trainingImages.read((char*)&magicNum, sizeof(magicNum));
		magicNum = reverseInt(magicNum);

		trainingImages.read((char*)&trainSize, sizeof(trainSize));
		trainSize = reverseInt(trainSize);

		trainingImages.read((char*)&n_rows, sizeof(n_rows));
		n_rows = reverseInt(n_rows);

		trainingImages.read((char*)&n_cols, sizeof(n_cols));
		n_cols = reverseInt(n_cols);

		if (trainingLabels.is_open())
		{
			for (int i = 0; i < trainSize; i++)
			{
				unsigned char temp = 0;
				trainingImages.read((char*)&temp, sizeof(temp));
				trainImageLabels[i] = (double)temp;
				//cout << "training label " << (double)temp << endl;
			}

		}
		
		for (int i = 0; i < trainSize; i++)
		{
			for (int r = 0; r < n_rows; r++)
				for (int c = 0; c < n_cols; c++)
				{
					unsigned char temp = 0;
					trainingImages.read((char*)&temp, sizeof(temp));
					trainImagePixels[i][r][c] = (int)temp / 255.0;
				}
			//cout << "Training : Read " << i << " images" << endl;
		}
		trainingImages.close();
		
	}
	
	cout << "Read Testing Images and Labels!" << endl;
	// +--------------------------------------------------+
	// |          Read Testing Images and Labels          |
	// +--------------------------------------------------+
	ifstream testingImages("data/t10k-images-idx3-ubyte");
	ifstream testingLabels("data/t10k-labels-idx1-ubyte");
	if (testingImages.is_open())
	{
		int magicNum = 0;
		int testSize = 0;
		int n_rows = 0;
		int n_cols = 0;

		testingImages.read((char*)&magicNum, sizeof(magicNum));
		magicNum = reverseInt(magicNum);

		testingImages.read((char*)&testSize, sizeof(testSize));
		testSize = reverseInt(testSize);

		testingImages.read((char*)&n_rows, sizeof(n_rows));
		n_rows = reverseInt(n_rows);

		testingImages.read((char*)&n_cols, sizeof(n_cols));
		n_cols = reverseInt(n_cols);

		if (testingLabels.is_open())
		{
			for (int i = 0; i < testSize; i++)
			{
				unsigned char temp = 0;
				testingLabels.read((char*)&temp, sizeof(temp));
				testImageLabels[i] = (int)temp;
			}

		}

		for (int i = 0; i < testSize; i++)
		{
			for (int r = 0; r < n_rows; r++)
				for (int c = 0; c < n_cols; c++)
				{
					unsigned char temp = 0;
					testingImages.read((char*)&temp, sizeof(temp));
					testImagePixels[i][r][c] = (int)temp / 255.0;
				}
			// cout << "Testing : Read " << i << " images" << endl;
		}
		testingImages.close();
	}
	cout << "End Reading" << endl;
	cout << "Begin Network Init" << endl;
	// +--------------------------------------------------+
	// |  Finished Reading, Begin Network Initialization  |
	// +--------------------------------------------------+

	// Convolutional layer Weights
	// double w[FRAMES][WINDOWSIZE][WINDOWSIZE];
	for (size_t i = 0; i < FRAMES; i++)
		for (size_t j = 0; j < WINDOWSIZE; j++)
			for (size_t k = 0; k < WINDOWSIZE; k++)
			{
				w[i][j][k] = double(rand()) / double(RAND_MAX) - 0.5;
				w_grad[i][j][k] = 0;
			}

	// Convolutional layer Biases
	// double b0[FRAMES];
	for (size_t i = 0; i < FRAMES; i++) 
	{
		b0[i] = double(rand()) / double(RAND_MAX) - 0.5;
		b0_grad[i] = 0;
	}

	// Fully Connected Layer 1 Weights
	// double u[HIDDEN][FRAMES][F2RES][F2RES];
	for (size_t i = 0; i < HIDDEN; i++)
		for (size_t j = 0; j < FRAMES; j++)
			for (size_t k = 0; k < F2RES; k++)
				for (size_t l = 0; l < F2RES; l++)
				{
					u[i][j][k][l] = double(rand()) / double(RAND_MAX) - 0.5;
					u_grad[i][j][k][l] = 0;
				}

	// Fully Connected Layer 1 Biases
	// double b1[HIDDEN];
	for (size_t i = 0; i < HIDDEN; i++)
	{
		b1[i] = double(rand()) / double(RAND_MAX) - 0.5;
		b1_grad[i] = 0;
	}

	// Fully Connected Layer 2 Weights
	// double v[OUT][HIDDEN];
	for (size_t i = 0; i < OUT; i++)
		for (size_t j = 0; j < HIDDEN; j++)
		{
			v[i][j] = double(rand()) / double(RAND_MAX) - 0.5;
			v_grad[i][j] = 0;
		}

	// Fully Connected Layer 2 Biases
	// double b2[OUT];
	for (size_t i = 0; i < OUT; i++)
	{
		b2[i] = double(rand()) / double(RAND_MAX) - 0.5;
		b2_grad[i] = 0;
	}
	cout << "End Network Init" << endl;
	// +--------------------------------------------------+
	// |            End Network Initialization            |
	// +--------------------------------------------------+

	//cout << "Begin Forward Propagation" << endl;
	// +--------------------------------------------------+
	// |            Start Forward Propagation             |
	// +--------------------------------------------------+

	// Init counts
	int correctCount = 0;
	double averageError = 0;
	double maxAcc = 0.0;

	for (size_t EP = 0; EP < EPOCHS; EP++)
	{
		cout << EP << endl;
		// Reset/Check cl_y_sum for maps
		if (EP == EPOCHS - 1)
		{
			for (size_t i = 0; i < FRAMES; i++)
				for (size_t j = 0; j < F1RES; j++)
					for (size_t k = 0; k < F1RES; k++)
					{
						cl_y_sum[i][j][k] = 0.0;
					}
		}

		correctCount = 0;
		averageError = 0;

		for (size_t II = 0; II < TRAINING; II++)
		{

			// Set imgInput
			// double imgInput[IMG_ROWS][IMG_COLS];
			for (size_t i = 0; i < IMG_ROWS; i++)
			{
				for (size_t j = 0; j < IMG_COLS; j++)
				{
					imgInput[i][j] = trainImagePixels[II][i][j];	// train image pixels
				}
			}

			// imgInput to Convolutional Layer
			for (size_t i = 0; i < FRAMES; i++)
			{
				for (size_t j = 0; j < F1RES; j++)
				{
					for (size_t k = 0; k < F1RES; k++)
					{
						s0[i][j][k] = 0.0;
						for (size_t l = 0; l < WINDOWSIZE; l++)
						{
							for (size_t m = 0; m < WINDOWSIZE; m++)
							{
								s0[i][j][k] += imgInput[j + l][k + m] * w[i][l][m];
							}
						}

						s0[i][j][k] += b0[i];

						cl_y[i][j][k] = sigmoid(s0[i][j][k]);
						if (EP == EPOCHS - 1)   
							cl_y_sum[i][j][k] += cl_y[i][j][k];
					}
				}
			}

			// Convolutional Layer to Max Pooling Layer
			for (size_t i = 0; i < FRAMES; i++)
			{
				for (size_t j = 0; j < F2RES; j++)
				{
					for (size_t k = 0; k < F2RES; k++)
					{
						// Set Max to the first pixel in group.
						// pixel_track holds that pixels location in cl_y
						double max = cl_y[i][j * 2][k * 2];
						pixel_track[i][j][k] = 0;
						if (cl_y[i][j * 2][(k * 2) + 1] > max)
						{
							max = cl_y[i][j * 2][(k * 2) + 1];
							pixel_track[i][j][k] = 1;
						}
						if (cl_y[i][(j * 2) + 1][k * 2] > max)
						{
							max = cl_y[i][(j * 2) + 1][k * 2];
							pixel_track[i][j][k] = 2;
						}
						if (cl_y[i][(j * 2) + 1][(k * 2) + 1] > max)
						{
							max = cl_y[i][(j * 2) + 1][(k * 2) + 1];
							pixel_track[i][j][k] = 3;
						}
						pool_layer[i][j][k] = max;
					}
				}
			}

			// Max Pooling Layer to Fully Connect Layer 1
			// double u[HIDDEN][FRAMES][F2RES][F2RES];
			for (size_t i = 0; i < HIDDEN; i++)
			{
				s1[i] = 0;
				for (size_t j = 0; j < FRAMES; j++)
				{
					for (size_t k = 0; k < F2RES; k++)
					{
						for (size_t l = 0; l < F2RES; l++)
						{
							s1[i] += pool_layer[j][k][l] * u[i][j][k][l];
						}
					}
				}
				s1[i] += b1[i];

				fl_y0[i] = sigmoid(s1[i]);
			}

			// Fully Connect Layer 1 to Fully Connect Layer 2
			for (size_t i = 0; i < OUT; i++)
			{
				s2[i] = 0;
				for (size_t j = 0; j < HIDDEN; j++)
				{
					s2[i] += fl_y0[j] * v[i][j];
				}

				s2[i] += b2[i];

				fl_y1[i] = sigmoid(s2[i]);
			}

			// z Results of Feed Forward
			for (size_t i = 0; i < OUT; i++)
			{
				z[i] = fl_y1[i];
			}

			//cout << "End Forward Propagation" << endl;
			// +--------------------------------------------------+
			// |             End Forward Propagation              |
			// +--------------------------------------------------+


			//cout << "Begin Back Propagation " << endl;
			// +--------------------------------------------------+
			// |             Begin Back Propagation               |
			// +--------------------------------------------------+
			// Set answer
			for (size_t i = 0; i < OUT; i++) 
			{
				if (trainImageLabels[II] == i)
				{
					answer[i] = 1;
				}
				else
				{
					answer[i] = 0;
				}
			}

			// Get/Check answer
			double highest = z[0];
			int highestIndex = 0;
			for (size_t i = 0; i < OUT; i++) 
			{
				if (highest < z[i]) 
				{
					highest = z[i];
					highestIndex = i;
				}
			}
			if (answer[highestIndex]) correctCount++;

			// Get error
			for (size_t i = 0; i < OUT; i++)
			{
				error[i] = z[i] - answer[i];
				averageError += error[i] * error[i] * 0.5;
			}

			// Update b2 (z Biases)
			// double b2[OUT];
			// de/dH2o * dH2o/dH2s * dH2s/dFCL2B[i]
			double l = 1;
			for (size_t i = 0; i < OUT; i++) 
			{
				z_error[i] = (LEARNRATE / l) * error[i] * (fl_y1[i] * (1.0 - fl_y1[i]));  // Sigmoid
				
				b2_grad[i] += z_error[i];
			}

			// Update v  HIDDEN to z)
			// double v[OUT][HIDDEN];
			// de/dH2o * dH2o/dH2s * dH2s/dFCLW[i][j]
			for (size_t i = 0; i < OUT; i++) 
			{
				for (size_t j = 0; j < HIDDEN; j++)
				{
					v_grad[i][j] += z_error[i] * fl_y0[j];
				}
			}

			// Update b1  HIDDEN Biases)
			// double b1[HIDDEN];
			// Σ[de/dH2o * dH2o/dH2s * dH2s/dH1o] * dH1o/dH1s * dH1s/dH1b
			// hidden_sum_error = Σ[de/dH2o * dH2o/dH2s * dH2s/dH1o] * dH1o/dH1s
			for (size_t i = 0; i < HIDDEN; i++)
			{
				double errorSum = 0.0;
				for (size_t j = 0; j < OUT; j++) 
				{
					errorSum += z_error[j] * v[j][i];
				}
				hidden_sum_error[i] = errorSum * (fl_y0[i] * (1.0 - fl_y0[i]));     // Sigmoid
				b1_grad[i] += hidden_sum_error[i];
			}

			// Update u  HIDDEN Weights)
			// double u[HIDDEN][FRAMES][F2RES][F2RES];
			// Σ[de/dH2o * dH2o/dH2s * dH2s/dH1o] * dH1o/dH1s * dH1s/dFCL1W
			for (size_t i = 0; i < HIDDEN; i++)
			{
				for (size_t j = 0; j < FRAMES; j++) 
				{
					for (size_t k = 0; k < F2RES; k++) 
					{
						for (size_t l = 0; l < F2RES; l++)
						{
							u_grad[i][j][k][l] += hidden_sum_error[i] * pool_layer[j][k][l];
						}
					}
				}
			}

			// Update b0
			// double b0[FRAMES];
			// Get the [W][K] Area
			// double pixel_track[FRAMES][F2RES][F2RES];   // Pooling Layer "Pixel Tracker" (0-3) ((2*j) + i)
			// double pool_layer[FRAMES][F2RES][F2RES];    // Pooling Layer Values
			for (size_t i = 0; i < FRAMES; i++)
			{
				first_layer_sum_error[i] = 0.0;
				for (size_t j = 0; j < F2RES; j++)
				{
					for (size_t k = 0; k < F2RES; k++) 
					{
						double errorSum = 0.0;
						cl_error[i][j][k] = 0.0;
						for (size_t l = 0; l < HIDDEN; l++) 
						{
							errorSum += hidden_sum_error[l] * u[l][i][j][k];
						}
						cl_error[i][j][k] = errorSum * (pool_layer[i][j][k] * (1.0 - pool_layer[i][j][k]));
						first_layer_sum_error[i] += errorSum * (pool_layer[i][j][k] * (1.0 - pool_layer[i][j][k]));  // Sigmoid
					}
				}
				b0_grad[i] += first_layer_sum_error[i];
			}

			// w
			// double w[FRAMES][WINDOWSIZE][WINDOWSIZE];
			for (size_t i = 0; i < FRAMES; i++) 
			{
				for (size_t j = 0; j < WINDOWSIZE; j++) 
				{
					for (size_t k = 0; k < WINDOWSIZE; k++)
					{
						double errorSum = 0.0;
						for (size_t m = 0; m < F2RES; m++)
						{
							for (size_t n = 0; n < F2RES; n++) 
							{
								int i3 = (int)pixel_track[i][m][n] / 2;
								int j3 = (int)pixel_track[i][m][n] % 2;
								errorSum += cl_error[i][m][n] * imgInput[2 * m + j + i3][2 * n + k + j3];
							}
						}
						w_grad[i][j][k] += errorSum;
					}
				}
			}

			//cout << "End Back Propagation " << endl;
			// +--------------------------------------------------+
			// |              End Back Propagation                |
			// +--------------------------------------------------+
			// Assign Grads

			if ((II + 1) % MINIBATCHES == 0)
			{
				for (size_t i = 0; i < OUT; i++) 
				{
					b2[i] -= (b2_grad[i] / MINIBATCHES);
					b2_grad[i] = 0;
				}

				for (size_t i = 0; i < OUT; i++)
				{
					for (size_t j = 0; j < HIDDEN; j++)
					{
						v[i][j] -= (v_grad[i][j] / MINIBATCHES);
						v_grad[i][j] = 0;
					}
				}

				for (size_t i = 0; i < HIDDEN; i++)
				{
					b1[i] -= (b1_grad[i] / MINIBATCHES);
					b1_grad[i] = 0;
				}

				for (size_t i = 0; i < HIDDEN; i++)
				{
					for (size_t j = 0; j < FRAMES; j++) 
					{
						for (size_t k = 0; k < F2RES; k++) 
						{
							for (size_t l = 0; l < F2RES; l++)
							{
								u[i][j][k][l] -= (u_grad[i][j][k][l] / MINIBATCHES);
								u_grad[i][j][k][l] = 0;
							}
						}
					}
				}

				for (size_t i = 0; i < FRAMES; i++) 
				{
					b0[i] -= (b0_grad[i] / MINIBATCHES);
					b0_grad[i] = 0;
				}

				for (size_t i = 0; i < FRAMES; i++) 
				{
					for (size_t l = 0; l < WINDOWSIZE; l++)
					{
						for (size_t m = 0; m < WINDOWSIZE; m++)
						{
							w[i][l][m] -= (w_grad[i][l][m] / MINIBATCHES);
							w_grad[i][l][m] = 0;
						}
					}
				}
			}
			if (II % 1000 == 0 && II != 0) 
			{
				cout << "Epoch " << EP << " ---- Image " << II << " ---- Accuracy " << ((double)correctCount / II) * 100  << " ---- error " << (averageError / II) * 100  << endl;
			}
		}
	}



	return 0;
}

int reverseInt(int i)
{
	unsigned char c1, c2, c3, c4;

	c1 = i & 255;
	c2 = (i >> 8) & 255;
	c3 = (i >> 16) & 255;
	c4 = (i >> 24) & 255;

	return ((int)c1 << 24) + ((int)c2 << 16) + ((int)c3 << 8) + c4;
}

double sigmoid(double x) 
{
	return (1 / (1 + exp(-1 * x)));
}