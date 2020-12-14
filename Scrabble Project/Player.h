/*
 * Player.h
 *
 *  Created on: Sep 18, 2016
 *      Author: kempe
 */
#ifndef PLAYER_H_
#define PLAYER_H_

#include <vector>
#include <set>
#include <string>
#include <fstream>
#include <sstream>
#include <iostream>
#include <cstdlib>
#include <stdexcept>
#include "Bag.h"
#include "Tile.h"

class Player {

public:
	Player (Bag bag, int i);
	~Player ();

	void setName(int i);
	string getName();
	void addTile (Tile *tile);
	void addTiles (std::vector<Tile*> tiles);
	int setScore(int add);
	int getScore () const;

private:
	std::vector<Tile*> held_tiles;
	string name;//name of this player
	int pScore = 0;//score of this player

};


#endif /* PLAYER_H_ */
