struct Node {
    int value;
    Node *next;
};

Node* fullsort (Node* in);
{
	fullsortHelper(in, in);
	return in;
}



Node* fullsortHelper(Node* oldHead, Node* in)
{
	if(in->next == NULL)//if whole list is sorted
	{
		return in;//return the original head/list
	}
	else
	{
		if(in->value > in->next->value)//find pivot point
		{
			attachHelper(in->next, oldHead);
			in->next = NULL;
		}
		else
		{
			fullsortHelper(oldHead, in->next);
		}
	}
}

void attachHelper(Node* curr, oldHead)
{
	if(curr->next == NULL)
	{
		curr->next = oldHead;
	}
	else
	{
		attachHelper(curr->next,oldHead);
	}
}
