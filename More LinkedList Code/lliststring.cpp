#include "lliststring.h"
#include <cstdlib>
#include <stdexcept>

LListString::LListString()
{
  head_ = NULL;
  tail_ = NULL;
  size_ = 0;
}

LListString::~LListString()
{
  clear();
}

bool LListString::empty() const
{
  return size_ == 0;
}

int LListString::size() const
{
  return size_;
}

/**
 * Complete the following function
 */
void LListString::insert(int pos, const std::string& val)
{
  if(pos < 0 || pos > size_)
  {
    throw std::invalid_argument("bad location");
  }
  else if(pos == 0)//at the head of the list
  {
    Item *temp = getNodeAt(pos);
    Item *newNode = new Item;
    newNode = NULL;
    newNode->prev = NULL;
    newNode->next = temp;
    temp->prev = newNode;
    head_ = newNode;
    newNode->val = val;
    size_++;
  }
  else if(pos == size_)//at the end of a list
  {
    Item *temp = getNodeAt(pos-1);
    Item *newNode = new Item;
    newNode = NULL;
    newNode->prev = temp;
    temp->next = newNode;
    newNode->next = NULL;
    newNode->val = val;
    size_++;
  }
  else//everywhere else between 0 and size
  {
    Item *temp = getNodeAt(pos);
    Item *newNode = new Item;
    newNode = NULL;
    newNode->prev = temp->prev;//set newNode's prev to temp's prev
    temp->prev->next = newNode;//set temp's prev's next to newNode
    temp->prev = newNode;//set temp's prev to point to newNode
    newNode->next = temp;//set newNode's temp to point to temp
    newNode->val = val;
    size_++;
  }
}

/**
 * Complete the following function
 */
void LListString::remove(int pos)
{
  if(pos < 0 || pos > size_)
  {
    throw std::invalid_argument("bad location");
  }
  else if(pos == 0)//at the head of the list
  {
    Item *temp = getNodeAt(pos);
    head_ = temp->next;
    temp->next->prev = NULL;
    delete temp;
    size_--;
  }
  else if(pos == size_)//at the end of a list
  {
    Item *temp = getNodeAt(pos);
    tail_ = temp->prev;
    temp->prev->next = NULL;
    delete temp;
    size_--;
  }
  else
  {
    Item *temp = getNodeAt(pos);
    temp->prev->next = temp->next;
    temp->next->prev = temp->prev;
    temp->next = NULL;
    temp->prev = NULL;
    delete temp;
    size_--;
  }
}

void LListString::set(int pos, const std::string& val)
{
  if(pos >= 0 && pos < size_) {
    Item *temp = getNodeAt(pos);
    temp->val = val;  
  }
}

std::string& LListString::get(int pos)
{
  if(pos < 0 || pos >= size_) {
    throw std::invalid_argument("bad location");
  }
  Item *temp = getNodeAt(pos);
  return temp->val;
}

std::string const & LListString::get(int pos) const
{
  if(pos < 0 || pos >= size_) {
    throw std::invalid_argument("bad location");
  }
  Item *temp = getNodeAt(pos);
  return temp->val;
}

void LListString::clear()
{
  while(head_ != NULL) {
    Item *temp = head_->next;
    delete head_;
    head_ = temp;
  }
  tail_ = NULL;
  size_ = 0;
}


/**
 * Complete the following function
 */
LListString::Item* LListString::getNodeAt(int pos) const
{
  int i = 0;
  if(head_ == NULL)
  {
    return head_;
  }
  else
  {
    Item *temp = head_;
    while(i < pos)
    {
      if(temp!=NULL)
      {
        temp = temp->next;
      } 
    }
    return temp;
  }
}
