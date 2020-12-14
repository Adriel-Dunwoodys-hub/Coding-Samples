// alistint.cpp : Defines the entry point for the console application.
//

#include "alistint.h"
#include "stackint.h"
#include <iostream>
#include <stdlib.h>
#include <fstream>
#include <string>
#include <stack>

using namespace std;



int main(int argc, _char *argv[])
{
  
	return 0;
}

//default constructor
AListInt::AListInt()
{
	int *_data;
	unsigned int _size = 0;
	unsigned int _cap = 5;
	_data[_cap];
}

//constructor
AListInt::AListInt(int cap)
{
	int* _data;
	unsigned int _size = 0;
	unsigned int _cap = cap;
	_data[_cap];
}

//Copy Constructor
AListInt::AListInt(const AListInt& other)
{
	_data = new int;
	this->_size = other._size;
	this->_cap = other._cap;
}

StackInt::StackInt()
{
}
StackInt::~StackInt()
{
}

AListInt& AListInt::operator=(const AListInt& other)
{
	AListInt(other);
}

//Destructor
AListInt::~AListInt()
{
	delete _data;
}


int AListInt::size() const
{
	return _size;
}

bool AListInt::empty() const
{
	if(_size > 0)
	{
		return false;
	}
	return true;
}

void AListInt::insert (int pos, const int& val)//this is wrong. need to shift after placement not ovverriding
{
	if(_size==_cap)//if arraylist is full
	{
		resize();
	}
	if(pos == _size+1 && pos < _cap)//if insertion is at the end
	{
		_data[pos] = val;
		_size++;
	}
	else
	if(pos < _cap && pos >= 0 && pos < _size)//if insertion is anywhere else shift to the right then insert
	{
		for(int i = pos+1; i < _size; i++)
		{
			_data[i] = _data[i+1];//accurate?
		}
		_data[pos] = val;
		_size++;
	}
}

void AListInt::remove(int pos)//just set to NULL?
{
	if(pos < _cap && pos >= 0)
	{
		_data[pos] = NULL;
	}
	else
	{
		cout << "Out of bounds error! \n";
	}
}

/**
   * Overwrites the old value at index, pos, with val
*/
void AListInt::set (int position, const int& val)//within the size
{
	if(position < _size && position < _cap && position >= 0)
	{
		_data[position] = val;
	}
}

  /**
   * Returns the value at index, pos
   */
int& AListInt::get (int position)
{
	if(position < _size && position >= 0 && _data[position]!=NULL)
	{
		return _data[position];
	}
	else
	{
		cout << "Out of bounds error! \n";
	}
}

int const& AListInt::get (int position) const
{
	if(position < _cap && position >= 0 && _data[position]!=NULL)
	{
		return _data[position];
	}
	else
	{
		cout << "Out of bounds error! \n";
	}
}

AListInt AListInt::operator+(const AListInt& other) const
{
	int k = 0;
	AListInt newarray;//why cant get this?
	newarray._data[this->_size+other._size];
	newarray._cap = this->_size+other._size;
	for(int i = 0; i < this->_size;i++)
	{
		newarray._data[i] = this->get(i);
		newarray._size++;
	}
	for(int j = this->_size+1; j < other._size+this->_size;j++)//ask if this works
	{
		newarray._data[j] = other.get(k);
		newarray._size++;
		k++;
	}
	return newarray;
}

int const & AListInt::operator[](int position) const//not sure what to do for this method
{
	if(position < _cap && position >= 0 && _data[position]!=NULL)
	{
		return _data[position];
	}
	else
	{
		cout << "Out of bounds error! \n";
	}
}

int& AListInt::operator[](int position)//not sure what to do for this method
{
	if(position < _cap && position >= 0 && _data[position]!=NULL)
	{
		return _data[position];
	}
	else
	{
		cout << "Out of bounds error! \n";
	}
}
void AListInt::resize()
{
	//resizing correctly
		int* resize = new int[_cap*2];
		_cap = _cap * 2;
		for(int i = 0; i < _size; i++)
		{
			resize[i] = _data[i];
		}
		delete[] _data;
		_data = resize;//would this call the operator function
}