#include "LLDeque.h"

using namespace std;

LLDeque()
{
	size = 0;
}

LLDeque()
{
	Node *temp = head;
	while(temp->next !=NULL)
	{
		head = temp->next;
		delete temp;
		temp = head;
	}
	delete temp;
	delete head;
	delete tail;
	size = 0;
}

void pushback (char x)
{	
	if(size == 0)//empty  deque
	{
		Node *temp = Node(x);//create a node with null next and prev pointers
		head = temp;
		tail = temp;
		size++;
		delete temp;
	}
	else if(size > 0)
	{
		Node *newNode = Node(x);
		Node *temp;
		temp = tail;
    	temp->next = newNode;
    	newNode->prev = temp;
    	tail = newNode;
    	size++;
    	delete temp;
    	delete newNode;
	}
}

void popback()
{
	if(size == 1)
	{
		delete *head;
		head = NULL;
		tail = NULL;
		size--;
	}
	else if(size > 1)
	{
		Node *temp = tail;
		temp->prev->next = NULL;
		tail = temp->prev;
		delete *temp;//delete what temp is pointing at not just temp pointer
		delete temp;
		size--;
	}
}

char getback()
{
	return tail->data;
}

void pushfront(char x)
{
	if(size == 0)
	{
		Node *temp = Node(x);//create a node with null next and prev pointers
		head = temp;
		tail = temp;
		size++;
		delete temp;
	}
	else if(size > 0)
	{
		Node *newNode = Node(x);
		Node *temp;
		temp = head;
    	temp->prev = newNode;
    	newNode->next = temp;
    	head = newNode;
    	size++;
    	delete temp;
    	delete newNode;
	}
}

void popfront()
{
	if(size == 1)
	{
		delete *head;
		head = NULL;
		tail = NULL;
		size--;
	}
	else if(size > 1)
	{
		Node *temp = head;
		temp->next->prev = NULL;
		head = temp->next;
		delete *temp;//delete what temp is pointing at not just temp pointer
		delete temp;
		size--;
	}
}

char getfront()
{
	return head->data;
}

bool isempty()
{
	if(size == 0){
		return true;
	}
	return false;
}
