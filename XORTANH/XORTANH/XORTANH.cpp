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
	int X1[2000], X2[2000], T[2000], z1;
	float s1, s2, r, y1, y2, z, w11, w21, w12, w22, u1, u2, u3, w110, w210, w120, w220, u10, u20, e;
	float b0, b, b1, b2, b10, b20;

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
	u10 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "u1 = " << u10 << endl;
	u20 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "u20 = " << u20 << endl;
	b10 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "b1 = " << b10 << endl;
	b20 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "b2 = " << b20 << endl;
	b0 = float(rand()) / float(RAND_MAX) - 0.5;
	outfile << "b = " << b0 << endl << endl;

	// learning rate = 0.1
	e = 0.1;

	for (i = 0; i < 2000; i++)
	{
		//cout <<  ((double)rand() / (RAND_MAX))  << endl;
		// 0 and 1 values for XOR
		r1 = rand() % 2;
		X1[i] = r1;
		r2 = rand() % 2;
		X2[i] = r2;

		// XOR Truth Table operation
		if (X1[i] == X2[i])
			T[i] = 0;
		else
			T[i] = 1;

		outfile << endl << "x1 x2 | XOR" << endl;
		outfile << X1[i] << "  " << X2[i] << "  |  " << T[i] << endl;

		// Neural Network equations for
		// w11, w21, w12, w22
		// b, b1, b2
		// S1, S2, y1, y2, u1, u2
		// z
		s1 = w110 * X1[i] + w210 * X2[i];
		s2 = w120 * X1[i] + w220 * X2[i];
		y1 = tanh(s1 + b10);
		y2 = tanh(s2 + b20);
		r = u10 * y1 + u20 * y2;
		z = tanh(r + b0);
		u1 = u10 - e * (z - T[i]) * (1 - z * z) * y1;
		u2 = u20 - e * (z - T[i]) * (1 - z * z) * y2;
		w11 = w110 - e * (1.0 - z * z) * (1.0 - y1 * y1) * (z - T[i]) * u1 * X1[i];
		w21 = w210 - e * (1.0 - z * z) * (1.0 - y1 * y1) * (z - T[i]) * u1 * X2[i];
		w12 = w120 - e * (1.0 - z * z) * (1.0 - y2 * y2) * (z - T[i]) * u2 * X1[i];
		w22 = w220 - e * (1.0 - z * z) * (1.0 - y2 * y2) * (z - T[i]) * u2 * X2[i];
		b = b0 - e * (z - T[i]) * (1 - z * z);
		b1 = b10 - e * (z - T[i]) * (1.0 - z * z) * (1.0 - y1 * y1) * u1;
		b2 = b20 - e * (z - T[i]) * (1.0 - z * z) * (1.0 - y2 * y2) * u2;

		outfile << "w11 = " << w11 << " | w21 = " << w21 << " | w12 = " << w12 << " | w22 = " << w22 << " | u1 = " << u1 << " | u2 = " << u2 << endl;

		w110 = w11;
		w210 = w21;
		w120 = w12;
		w220 = w22;
		u10 = u1;
		u20 = u2;
		b0 = b;
		b10 = b1;
		b20 = b2;

		outfile << "s1 = " << s1 << " | s2 = " << s2 << " | y1 = " << y1 << " | y2 = " << y2 << " | z = " << z << " | error = " << z - T[i] << endl << endl;
	}

	for (j = 0; j < 2; j++)
	{
		outfile << "*************************************" << endl;
		outfile << "BEGINING OF EPOCH " << j + 1 << endl;
		outfile << "*************************************" << endl;

		for (i = 0; i < 2000; i++)
		{
			outfile << endl << "x1 x2 | XOR" << endl;
			outfile << X1[i] << "  " << X2[i] << "  |  " << T[i] << endl;

			s1 = w110 * X1[i] + w210 * X2[i];
			s2 = w120 * X1[i] + w220 * X2[i];
			y1 = tanh(s1 + b10);
			y2 = tanh(s2 + b20);
			r = u10 * y1 + u20 * y2;
			z = tanh(r + b0);
			u1 = u10 - e * (z - T[i]) * (1 - z * z) * y1;
			u2 = u20 - e * (z - T[i]) * (1 - z * z) * y2;
			w11 = w110 - e * (1.0 - z * z) * (1.0 - y1 * y1) * (z - T[i]) * u1 * X1[i];
			w21 = w210 - e * (1.0 - z * z) * (1.0 - y1 * y1) * (z - T[i]) * u1 * X2[i];
			w12 = w120 - e * (1.0 - z * z) * (1.0 - y2 * y2) * (z - T[i]) * u2 * X1[i];
			w22 = w220 - e * (1.0 - z * z) * (1.0 - y2 * y2) * (z - T[i]) * u2 * X2[i];
			b = b0 - e * (z - T[i]) * (1 - z * z);
			b1 = b10 - e * (z - T[i]) * (1.0 - z * z) * (1.0 - y1 * y1) * u1;
			b2 = b20 - e * (z - T[i]) * (1.0 - z * z) * (1.0 - y2 * y2) * u2;

			outfile << "w11 = " << w11 << " | w21 = " << w21 << " | w12 = " << w12 << " | w22 = " << w22 << " | u1 = " << u1 << " | u2 = " << u2 << " | b = " << b << " | b1 = " << b1 << " | b2 = " << b2 << endl;

			// update old weights
			w110 = w11;
			w210 = w21;
			w120 = w12;
			w220 = w22;
			u10 = u1;
			u20 = u2;
			b0 = b;
			b10 = b1;
			b20 = b2;

			outfile << "S1 = " << s1 << " | S2 = " << s2 << endl;
			outfile << "y1 = " << y1 << " | y2 = " << y2 << endl;
			outfile << "Z  = " << z << " | Error 1 = " << z - T[i] << endl;
		}

		outfile << "*************************************" << endl;
		outfile << "END OF EPOCH " << j + 1 << endl;
		outfile << "*************************************" << endl;
	}
	return 0;
}
