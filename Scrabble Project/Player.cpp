/*
 * Player.cpp
 *
 */

#include <vector>
#include <set>
#include <string>
#include <fstream>
#include <sstream>
#include <iostream>
#include <cstdlib>
#include <stdexcept>
#include "Tile.h"
#include "Player.h"
#include "Bag.h"

using namespace std;

Player::Player (Bag bag, int i)
{
	srand (random_seed);
	void setName(i);
}

Player::~Player ()
{
	
}

void Player::addTile (Tile *tile)
{
	_tiles.push_back (tile);
	int j = rand() % _tiles.size();
	_tiles[_tiles.size()-1] = _tiles[j];
	_tiles[j] = tile;
}

void Player::addTiles (vector<Tile*> tiles)
{
	for (vector<Tile*>::iterator it = tiles.begin(); it != tiles.end(); ++it)
		addTile (*it);
}

void Player::setName (int i)
{
	name = "Player " << i; 
}

std::string Player::getName()
{
	return name;
}

void Player::setScore(int add)
{
	pScore += add;
}

int Player::getScore()
{
	return pScore;
}

std::set<Tile*> Player::drawTiles (int number)
{
	set<Tile*> tileSet;
	for (int i = 0; i < number && tilesRemaining() > 0; ++i)
	{
		tileSet.insert (_tiles.back());
		_tiles.pop_back();
	}
	return tileSet;
}
