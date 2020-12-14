#include <iostream>
#include <stdlib.h>
#include <fstream>
#include <string>
#include <locale>
#include <stdio.h>
#include <ctype.h>
#include <algorithm>
#include <functional>
#include <set>

using namespace std;

int main(int argc, char* argv[])
{
	int stringLength;
	int startingword;
	int startingnum;
	int endingnum;
	string check;
	string find;
	char c;
	char b;
	std::locale loc;
	bool lastword = false;
	bool terminate = false;
	std::set<std::string> s_string;
	ifstream input;

	cin >> check;

	input.open("input.txt");
	//reading in from file and putting it into the set
	while(!input.eof())
	{
		input >> check;
		stringLength = check.length();
		startingword = 0;
		startingnum = 0;
		for(int i = 0; i < stringLength; i++)
		{
			c = check[i];
			if(isalpha(c))//if it is a alphabetic char
			{

			}
			else//if it is not a alphabetic char
			{
				lastword = true;
				startingnum = i;
				for(int j = i+1; j < stringLength; j++)
				{
					b = check[j];
					if(isalpha(b))//if it is a alphabetic char
					{
						endingnum = j-1;
						break;
					}
				}
				string temp = check.substr(startingword,(startingnum-startingword));
				std::transform(temp.begin(), temp.end(), temp.begin(), ::tolower);//change the string to lowercase before adding it to the set
				s_string.insert(temp);
				startingword = endingnum+1;//new starting point from inside the string check
				i = endingnum + 1;//new starting point from inside the string check

			}
		}
		if(lastword)
		{
			string temp = check.substr(startingword,check.npos);
			std::transform(temp.begin(), temp.end(), temp.begin(), ::tolower);//change the string to lowercase before adding it to the set
			s_string.insert(temp);
		}
		else
		{
			std::transform(check.begin(), check.end(), check.begin(), ::tolower);//change the string to lowercase before adding it to the set
			s_string.insert(check);
		}
		do
		{
			cout << "Enter in the word you wish to find.(not case-sensitive) \n";
			cin >> find;
			std::transform(find.begin(), find.end(), find.begin(), ::tolower);//change the string to lowercase
			if (cin.get() == '\n')//user enters in empty string
			{
				terminate = true;//set it to true to leave the loop and finish the program
			}
			else//if find isnt empty string
			{
				if(s_string.count(find) > 0)//if it found string in set
				{
					cout << "In the file. \n";
				}
				else//couldnt find the string in set
				{
					cout << "Not in the file. \n";
				}
			}
		} while(!terminate);//while terminate doesnt = true
	}

	return 0;
}