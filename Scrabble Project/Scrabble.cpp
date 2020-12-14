/*
 * Scrabble.cpp
 *
 *  Created on: Sep 18, 2016
 *      Author: kempe
 */

// The following is a suggestion for how to proceed.
// Feel free to make any changes you like.

#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <cstdlib>
#include <stdexcept>

#include "Tile.h"
#include "Dictionary.h"
#include "Bag.h"
#include "Board.h"
#include "Player.h"

using namespace std;

void readConfigFile (string config_file_name,
					 string & dictionary_file_name,
					 string & board_file_name,
					 string & bag_file_name,
					 unsigned int & hand_size)
{
	ifstream configFile (config_file_name.c_str());
	string line;
    bool number = false, board = false, tiles = false, dictionary = false;

	if (!configFile.is_open())
		throw invalid_argument("Cannot open file: " + config_file_name);
	while (getline (configFile, line))
	{
		stringstream ss (line);
		string parameter;
		ss >> parameter;
		if (parameter == "NUMBER:")
			{ ss >> hand_size; number = true; }
		else if (parameter == "BOARD:")
		    { ss >> board_file_name; board = true; }
		else if (parameter == "TILES:")
			{ ss >> bag_file_name; tiles = true; }
		else if (parameter == "DICTIONARY:")
			{ ss >> dictionary_file_name; dictionary = true; }
	}
	if (!number)
		throw invalid_argument("Hand size not specified in config file");
	if (!board)
		throw invalid_argument("Board file name not specified in config file");
	if (!tiles)
		throw invalid_argument("Bag file name not specified in config file");
	if (!dictionary)
		throw invalid_argument("Dictionary file name not specified in config file");
}

int main (int nargs, char **args)
{
	bool endgame = false;
	int numOfPlayers;
	if (nargs < 2 || nargs > 2)
		cout << "Usage: Scrabble <config-filename>\n";
	try {
		string dictionaryFileName, boardFileName, bagFileName;
		unsigned int numTiles;

		readConfigFile (args[1],
						dictionaryFileName, boardFileName, bagFileName,
						numTiles);

		Dictionary dict (dictionaryFileName);
		Board board (boardFileName);
		Bag bag (bagFileName, 10); // second argument is random seed

		// Good luck!

		//find out how many players
		cout << "Please enter in a number from 1-8 on how many players will be playing." << endl;
		cin >> numOfPlayers;
		//create an array of x players
		Player arrayPlayers[numOfPlayers];
		//create x players and put into arrayPlayers and give them 7 tiles
		for (int i = 0; i < numOfPlayers; i++)
		{
			arrayPlayers[i] = Player(bag, i);
		}

		//bool value to know if game is at end or not
		while(endgame == false)
		{
			//begin looping through player array as it will be thier turn
			for(int i = 0; i < numOfPlayers; i++)
			{
				cout << "Player " << i << "'s turn." << endl;
				cout << "Tiles in hand: "<< endl;
			}
		}

		return 0;
	}
	catch (invalid_argument & e)
	{ cout << "Fatal Error! " << e.what(); }
	return 1;
}
