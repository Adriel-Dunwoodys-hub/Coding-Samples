// stackint.cpp : Defines the entry point for the console application.
//

#include "alistint.h"
#include "stackint.h"
#include <iostream>
#include <stdlib.h>
#include <fstream>
#include <string>
#include <stack>



int main(int argc,char *argv[])
{
  bool StackInt::empty() const
{
	return list_.empty();
}

  /**
   * Pushes a new value, val, onto the top of the stack
   */
void StackInt::push(const int& val)
{
	list_.insert(0,val);
}

  /**
   * Returns the top value on the stack
   */
const int &StackInt::top() const//how to declare this right
{
	list_.get(0);
}

  /**
   * Removes the top element on the stack
   */
void StackInt::pop()
{
	list_.remove(0);
}
	return 0;
}