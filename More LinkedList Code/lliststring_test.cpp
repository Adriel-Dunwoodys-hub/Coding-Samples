#include "lliststring.h"
#include <iostream>

using namespace std;

int main() {
  LListString* list = new LListString();

  // Check if the list is initially empty.
  if (list->empty()) {
    cout << "SUCCESS: List is empty initially." << endl;
  } else {
    cout << "FAIL: List is not empty initially when it should be." << endl;
  }

  // Insert an item at the head.
  list->insert(0, "Gandalf");

  // Check if the list is still empty.
  if (!list->empty()) {
    cout << "SUCCESS: List is not empty after one insertion." << endl;
  } else {
    cout << "FAIL: List is empty after attempted insertion." << endl;
  }

  if (list->size() == 1) {
    cout << "SUCCESS: List has size 1 after one insertion." << endl;
  } else {
    cout << "FAIL: List has size " << list->size() << " after one insertion.";
    cout << endl;
  }

  // Check if the value is correct.
  if (list->get(0) == "Gandalf") {
    cout << "SUCCESS: Gandalf is at the 0th index of the list." << endl;
  } else {
    cout << "FAIL: Gandalf is not at the 0th index of the list, " << list->get(0);
    cout << " is instead." << endl;
  }

  // TODO: Continue adding tests for your program, or start your own file with your
  // own tests. Remember to submit a file that is named correctly!

  // Insert an item at the tail.
  list->insert(1, "Boromir");

  if (list->size() == 2) {
    cout << "SUCCESS: List has size 2 after one insertions." << endl;
  } else {
    cout << "FAIL: List has size " << list->size() << " after two insertion.";
    cout << endl;
  }

  // Check if the value is correct.
  if (list->get(1) == "Boromir") {
    cout << "SUCCESS: Boromir is at the last index of the list." << endl;
  } else {
    cout << "FAIL: Boromir is not at the last index of the list, " << list->get(0);
    cout << " is instead." << endl;
  }

  // Insert an item in the middle of the list.
  list->insert(1, "One Ring to rule them all, One Ring to find them, One Ring to bring them all and in the darkness bind them");
  
  if (list->size() == 3) {
    cout << "SUCCESS: List has size 3 after one insertions." << endl;
  } else {
    cout << "FAIL: List has size " << list->size() << " after three insertion.";
    cout << endl;
  }

  // Check if the value is correct.
  if (list->get(1) == "One Ring to rule them all, One Ring to find them, One Ring to bring them all and in the darkness bind them") {
    cout << "SUCCESS: Phrase is at the middle index of the list." << endl;
  } else {
    cout << "FAIL: Phrase is not at the middle index of the list, " << list->get(0);
    cout << " is instead." << endl;
  }

  //remove from the middle
  list->remove(1);

  //check if remove is succesful
  if (list->size() == 2) {
    cout << "SUCCESS: List has size 2 after one deletion from 3." << endl;
  } else {
    cout << "FAIL: List has size " << list->size() << " after one deletion.";
    cout << endl;
  }
  // Clean up memory.
  delete list;
}
