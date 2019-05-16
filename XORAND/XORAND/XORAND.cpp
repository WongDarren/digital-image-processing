// XORTANH.cpp : This file contains the 'main' function. Program execution begins and ends there.

#include "pch.h"
#include <iostream>
#include <fstream>
#include <cmath>

using std::ofstream;
using namespace std;

int main()
{
	ofstream outfile("file2.dat");

	int i, j, r1, r2;
	// 2000 X1 inputs, 2000 X2 inputs, 2000 Truth values
	int X1[2000], X2[2000], T1[2000], T2[2000];
	float s1, s2, s3, s4,
		y1, y2,
		w11, w21, w12, w22,
		u11, u12, u21, u22, 
		w110, w120, w210, w220, 
		u110, u120, u210, u220,
		z1, z2,
		e;
	float b1, b2, b3, b4,
		b10, b20, b30, b40;

	outfile << "INT_MAX = " << INT_MAX << endl;
	outfile << "RAND_MAX = " << RAND_MAX << endl << endl;

	// Assigning random value from -0.5 to 0.5 
	w110 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "w11 = " << w110 << endl;
	w210 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "w21 = " << w210 << endl;
	w120 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "w12 = " << w120 << endl;
	w220 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "w22 = " << w220 << endl;

	u110 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "u11 = " << u110 << endl;
	u120 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "u12 = " << u120 << endl;
	u210 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "u21 = " << u210 << endl;
	u220 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "u22 = " << u220 << endl;

	b10 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "b1 = " << b10 << endl;
	b20 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "b2 = " << b20 << endl;
	b30 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "b3 = " << b30 << endl;
	b40 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "b4 = " << b40 << endl << endl;

	// learning rate = 0.1
	e = 0.1;

	for (i = 0; i < 2000; i++)
	{
		// 0 and 1 values for X1 and X2
		r1 = rand() % 2;
		X1[i] = r1;
		r2 = rand() % 2;
		X2[i] = r2;

		// XOR Truth Table operation
		if (X1[i] == X2[i])
			T1[i] = 0;
		else
			T1[i] = 1;

		// AND Truth Table operation
		if (X1[i] == 1 && X2[i] == 1)
			T2[i] = 1;
		else
			T2[i] = 0;

		// print truth table for this iteration
		outfile << "x1 x2 | XOR AND" << endl;
		outfile << X1[i] << "  " << X2[i] << "  |  " << T1[i] << "   " << T2[i] << endl;

		// Neural Network equations

		// S1 and S2 equations
		s1 = w110 * X1[i] + w210 * X2[i] + b10;
		s2 = w120 * X1[i] + w220 * X2[i] + b20;

		// y1 and y2 equations
		y1 = tanh(s1);
		y2 = tanh(s2);

		// S3 and S4 equations
		s3 = y1 * u110 + y2 * u210 + b30;
		s4 = y1 * u120 + y2 * u220 + b40;

		// z1 and z2
		z1 = tanh(s3);
		z2 = tanh(s4);

		// Found u values using backward propagation
		u11 = u110 - e * (z1 - T1[i]) * (1 - z1 * z1) * y1;
		u12 = u120 - e * (z2 - T2[i]) * (1 - z2 * z2) * y2;
		u21 = u210 - e * (z1 - T1[i]) * (1 - z1 * z1) * y2;
		u22 = u220 - e * (z2 - T2[i]) * (1 - z2 * z2) * y1;

		// Found w values using backward propagation
		w11 = w110 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u11 * (1 - y1 * y1) * X1[i] + (z2 - T2[i]) * (1 - z2 * z2) * u12 * (1 - y1 * y1) * X1[i]);
		w21 = w210 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u11 * (1 - y1 * y1) * X2[i] + (z2 - T2[i]) * (1 - z2 * z2) * u12 * (1 - y1 * y1) * X2[i]);
		w12 = w120 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u21 * (1 - y2 * y2) * X1[i] + (z2 - T2[i]) * (1 - z2 * z2) * u22 * (1 - y2 * y2) * X1[i]);
		w22 = w220 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u21 * (1 - y2 * y2) * X2[i] + (z2 - T2[i]) * (1 - z2 * z2) * u22 * (1 - y2 * y2) * X2[i]);

		// Found b values using backward propagation
		b3 = b30 - e * (z1 - T1[i]) * (1 - z1 * z1);
		b4 = b40 - e * (z2 - T2[i]) * (1 - z2 * z2);
		b1 = b10 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u11 * (1 - y1 * y1) + (z2 - T2[i]) * (1 - z2 * z2) * u12 * (1 - y1 * y1));
		b2 = b20 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u12 * (1 - y2 * y2) + (z2 - T2[i]) * (1 - z2 * z2) * u22 * (1 - y2 * y2));

		// print w and u values
		outfile << "w11 = " << w11 << " | w21 = " << w21 << " | w12 = " << w12 << " | w22 = " << w22 << endl;
		outfile << "u11 = " << u11 << " | u21 = " << u21 << " | u12 = " << u12 << " | u22 = " << u22 << endl;

		// Assign new values to old values for next iteration
		w110 = w11;
		w210 = w21;
		w120 = w12;
		w220 = w22;
		
		u110 = u11;
		u120 = u12;
		u210 = u21;
		u220 = u22;

		b10 = b1;
		b20 = b2;
		b30 = b3;
		b40 = b4;

		// print S, y, Z, and error values
		outfile << "S1 = " << s1 << " | S2 = " << s2 << " | S3 = " << s3 << " | S4 = " << s4 << endl;
		outfile << "y1 = " << y1 << " | y2 = " << y2 << endl;
		outfile << "Z1 = " << z1 << " | Error 1 = " << z1 - T1[i] << endl;
		outfile << "Z2 = " << z2 << " | Error 2 = " << z2 - T2[i] << endl << endl;
	}

	for (j = 1; j < 3; j++)
	{
		outfile << "**************************************************************************" << endl;
		outfile << "BEGINING OF EPOCH " << j << endl;
		outfile << "**************************************************************************" << endl;

		for (i = 0; i < 2000; i++)
		{
			outfile << "x1 x2 | XOR AND" << endl;
			outfile << X1[i] << "  " << X2[i] << "  |  " << T1[i] << "   " << T2[i] << endl;

			s1 = w110 * X1[i] + w210 * X2[i] + b10;
			s2 = w120 * X1[i] + w220 * X2[i] + b20;

			y1 = tanh(s1);
			y2 = tanh(s2);

			s3 = y1 * u110 + y2 * u210 + b30;
			s4 = y1 * u120 + y2 * u220 + b40;

			z1 = tanh(s3);
			z2 = tanh(s4);

			u11 = u110 - e * (z1 - T1[i]) * (1 - z1 * z1) * y1;
			u12 = u120 - e * (z2 - T2[i]) * (1 - z2 * z2) * y2;
			u21 = u210 - e * (z1 - T1[i]) * (1 - z1 * z1) * y2;
			u22 = u220 - e * (z2 - T2[i]) * (1 - z2 * z2) * y1;

			w11 = w110 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u11 * (1 - y1 * y1) * X1[i] + (z2 - T2[i]) * (1 - z2 * z2) * u12 * (1 - y1 * y1) * X1[i]);
			w21 = w210 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u11 * (1 - y1 * y1) * X2[i] + (z2 - T2[i]) * (1 - z2 * z2) * u12 * (1 - y1 * y1) * X2[i]);
			w12 = w120 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u21 * (1 - y2 * y2) * X1[i] + (z2 - T2[i]) * (1 - z2 * z2) * u22 * (1 - y2 * y2) * X1[i]);
			w22 = w220 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u21 * (1 - y2 * y2) * X2[i] + (z2 - T2[i]) * (1 - z2 * z2) * u22 * (1 - y2 * y2) * X2[i]);

			b3 = b30 - e * (z1 - T1[i]) * (1 - z1 * z1);
			b4 = b40 - e * (z2 - T2[i]) * (1 - z2 * z2);
			b1 = b10 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u11 * (1 - y1 * y1) + (z2 - T2[i]) * (1 - z2 * z2) * u12 * (1 - y1 * y1));
			b2 - b10 - e * ((z1 - T1[i]) * (1 - z1 * z1) * u12 * (1 - y2 * y2) + (z2 - T2[i]) * (1 - z2 * z2) * u22 * (1 - y2 * y2));

			outfile << "w11 = " << w11 << " | w21 = " << w21 << " | w12 = " << w12 << " | w22 = " << w22 << endl;
			outfile << "u11 = " << u11 << " | u21 = " << u21 << " | u12 = " << u12 << " | u22 = " << u22 << endl;
			outfile << "b1  = " << b1 << " | b2  = " << b2 << " | b3  = " << b3 << " | b4  = " << b4 << endl;

			// update old weights
			w110 = w11;
			w210 = w21;
			w120 = w12;
			w220 = w22;

			u110 = u11;
			u120 = u12;
			u210 = u21;
			u220 = u22;

			b10 = b1;
			b20 = b2;
			b30 = b3;
			b40 = b4;

			outfile << "S1 = " << s1 << " | S2 = " << s2 << " | S3 = " << s4 << " | S3 = " << s4 << endl;
			outfile << "y1 = " << y1 << " | y2 = " << y2 << endl;
			outfile << "Z1 = " << z1 << " | Error 1 = " << z1 - T1[i] << endl;
			outfile << "Z2 = " << z2 << " | Error 2 = " << z2 - T2[i] << endl << endl;
		}

		outfile << "**************************************************************************" << endl;
		outfile << "END OF EPOCH " << j << endl;
		outfile << "**************************************************************************" << endl;
	}
	return 0;
}
