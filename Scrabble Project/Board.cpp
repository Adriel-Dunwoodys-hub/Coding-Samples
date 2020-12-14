/*
 * Board.cpp
 *
 *  Created on: Sep 18, 2016
 *      Author: kempe
 */

// The following is a suggestion for how to proceed.
// Feel free to make any changes you like.

#include <string>
#include <fstream>
#include <iostream>
#include <sstream>
#include <stdexcept>
#include "Board.h"

using namespace std;

Board::Board (string board_file_name)
{
	ifstream boardFile (board_file_name.c_str());
	string row;

	_x = _y = _startx = _starty = 0; // to appease compiler
	if (!boardFile.is_open())
		throw invalid_argument("Cannot open file: " + board_file_name);
	getline (boardFile, row);
	stringstream s1 (row);
	s1 >> _x >> _y;
	getline (boardFile, row);
	stringstream s2 (row);
	s2 >> _startx >> _starty;
	_startx --; _starty --;  // coordinates from 0 in array

	// Anything else you need to initialize?

	for (int i = 0 ; i < _y; ++ i)
	{
		getline (boardFile, row);
		for (int j = 0; j < _x; ++ j)
		{
			// Fill in the following based on how you store the board.
			if (i == _starty && j == _startx) { }
			else switch (row[j]) {
			case '.' :
			break;
			case '2' :
			break;
			case '3' :
			break;
			case 'd' :
			break;
			case 't' :
			break;
			default:
				string error = "Improper character in Board file: ";
				throw invalid_argument(error + row[j]);
			}
		}
	}
	boardFile.close ();
}
