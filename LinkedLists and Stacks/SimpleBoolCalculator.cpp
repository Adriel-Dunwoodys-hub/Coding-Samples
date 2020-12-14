// booleanparse.cpp : Defines the entry point for the console application.
//

#include "stackint.h"
#include "alistint.h"
#include <iostream>
#include <stdlib.h>
#include <fstream>
#include <string>
#include <locale>
#include <stdio.h>
#include <ctype.h>
#include <map>
#include <stack>

using namespace std;


int main(int argc,char *argv[])
{
	//initialized variables
	map <int, string> boolanswer;
	StackInt list;
	ifstream file1;
	ifstream file2;
	string mapvalue;
	int mapkey;
	int a = 0;
	int i = 0;
	string check;
	string added;//string to find length of a number that will be the mapkey
	char c;
	bool loop = true;
	bool malform = false;

	file2.open("2ndfile.txt");
	//read in the 2nd file
	while(!file2.eof())
	{
		file2 >> mapkey;//store the int for map key
		file2 >> mapvalue;//store string for map value
		boolanswer.insert(std::map<int, string>::value_type(mapkey, mapvalue));
	}
	file2.close();//close the file

	file1.open("1stfile.txt");
	while(!file1.eof())
	{
		getline(file1, check);
		i = 0;
		while(i < check.size())
		{
			c = check[i];
			switch(c)
			{
			case '(':
				malform = false;
				list.push(-1);
				break;
			case '&':
				malform = false;
				if(list.top() == check[i] || list.top() == -2 )//if duplicate
				{
					cout << "MALFORMED EXPRESSION!";
				}
				else
				{
					list.push(-2);
				}
				break;
			case '|':
				malform = false;
				if(list.top() == check[i] || list.top() == -3 )//if duplicate
				{
					cout << "MALFORMED EXPRESSION!";
				}
				else
				{
					list.push(-3);
				}
				break;
			case '~':
				malform = false;
				if(list.top() == -4)//if duplicate
				{
					list.pop();//pop the previous ~ sign as they cancel each other out
				}
				else
				{
					list.push(-4);
				}
				break;
			case ')'://evaluation
				while(list.top() != -1)//not (
				{
					int opperand2 = list.top();//1 or 0
					list.pop();
					int opperator = list.top();//-sign or bool operator
					list.pop();
					if(opperator == -4)//~
					{
						if(opperand2 == 1)
						{
							opperand2 = 0;
						}
						else
						{
							opperand2 = 1;
						}
						opperator = list.top();//itll either be & or |
						list.pop();
					}
					int opperand1 = list.top();//1 or 0
					list.pop();
					//check on more time if there is a - sign for opperand1
					if(list.top() == -4)
					{
						if(opperand1 == 1)//switch sign of opperand1
						{
							opperand1 = 0;
						}
						else
						{
							opperand1 = 1;
						}
						list.pop();//get rid of ~
					}
					if(opperator == -2)//&
					{
						if(opperand1 == 1 && opperand2 == 1)//both true
						{
							list.push(1);
						}
						else//will be false by default
						{
							list.push(0);
						}
					}
					else// |
					{
						if(opperand1 == 0 && opperand2 == 0)//both false
						{
							list.push(0);
						}
						else//will be true by default
						{
							list.push(1);
						}
					}
				}
				break;
			default:
				if(isdigit(c))//if it is a digit
				{
					if(malform)//if it is a double number
					{
						cout << "MALFORMED EXPRESSION!";
					}
					else
					{
						malform = true;
						//find out how big the number is by looking for continuous number sequence
						a = i;//start from 0
						while(loop)//is true
						{
							if(isdigit(check[a]))
							{
								added.append(check[a],0);//add int/char to string
								a++;//increment a to move through the string
							}
							else
							{
								int value = atoi(added.c_str());//convert added string into a int
								i = a;//let i continue where a left off
								map<int, string>::iterator iter = boolanswer.find(value);
								if(iter != boolanswer.end())
								{
									if(iter->second == "T")
									{
										list.push(1);
									}
									else
									{
										list.push(0);
									}
								}
								added = "";//make added string empty again to use again later
								break;//leave the while loop
							}
						}
					}
				}
				else//if not a number
				{
					cout << "MALFORMED EXPRESSION! \n";
				}

				break;//end of default
			}//end of switch
			i++;//increase i
		}
		cout << list.top();//final value of expression
		list.pop();//get rid of the final expression value
	}
	return 0;
}