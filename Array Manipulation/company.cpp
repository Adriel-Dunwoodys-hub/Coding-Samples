#include "company.h"

CompanyTracker::CompanyTracker (int n)
  // initializes the tracker with n students and their 1-person companies
{
  numCompanies = n;
  companies = new Company* [numCompanies];
  for (int i = 0; i < numCompanies; ++i)
      companies[i] = new Company ();
}

CompanyTracker::~CompanyTracker ()
  // deallocates all dynamically allocated memory
{
  // your implementation goes here
  for(int i = 0; i < numCompanies; ++i)
  {
    //using split to delete each company node leaving the single companies left
    split(i);
    delete companies[i]->merge1 = NULL;
    delete companies[i]->merge2 = NULL;
    delete companies[i]->parent = NULL;
    //delete the single company before moving on
    delete companies[i];
  }
  delete[] companies;//delete the array
}

void CompanyTracker::merge (int i, int j)
  /* Merges the largest companies containing students i and j,
     according to the rules described above.
     That is, it generates a new Company object which will
     become the parent company of the largest companies currently
     containing students i and j.
     If i and j already belong to the same company (or are the same),
     merge doesn't do anything.
     If either i or j are out of range, merge doesn't do anything. */
{
  //your implementation goes here
  if((i >= 0 && i < numCompanies) && j >= 0 && j < numCompanies)//valid input
  {
    if(!inSameCompany(i,j))//not in same company continue with merge
    {
      Company *iParent = NULL;
      Company *jParent = NULL; 
      if(companies[i]->parent != NULL)
      {
        //it has a parent, try to find largest parent
        iParent = findLargestParent(companies[i]->parent);
      }
      else
      {
        //it is already the largest parent
        iParent = companies[i];
      }
      if(companies[j]->parent != NULL)
      {
        //it has a parent, try to find the largest parent
        jParent = findLargestParent(companies[j]->parent);
      }
      else
      {
        //it is already the largest parent
        jParent = companies[j];
      }

      //creating new company with mergers and assigning new parent
      jParent->parent = iParent->parent = Company(iParent, jParent);

      delete iParent;
      delete jParent;
    }
  }
  else//invalid input
  {

  }
}

void CompanyTracker::split (int i)
  /* Splits the largest company that student i belongs to,
     according to the rules described above.
     That is, it deletes that Company object, and makes sure that
     the two subcompanies have no parent afterwards.
     If i's largest company is a 1-person company, split doesn't do anything.
     If i is out of range, split doesn't do anything. */
{
  // your implementation goes here
  if(i >= 0 && i < numCompanies)
  {
    //valid input
    Company *iParent = NULL;
    if(companies[i]->parent != NULL)
    {
      //it has a parent, find largest parent
      iParent = findLargestParent(companies[i]->parent);
      //set 1 level down children companies parent company to NULL
      iParent->merge1->parent = NULL;
      iParent->merge2->parent = NULL;

      //delete struct variables first then struct
      delete iParent->merge1;
      delete iParent->merge2;
      delete iParent->parent;
      delete iParent;
    }
    else
    {
      //it doesnt have parent and is largest company do nothing
      delete iParent;
    }
  }
}

bool CompanyTracker::inSameCompany (int i, int j)
  /* Returns whether students i and j are currently in the same company.
     Returns true if i==j.
     Returns false if i or j (or both) is out of range. */
{
  // your implementation goes here
  if((i >= 0 && i < numCompanies) && j >= 0 && j < numCompanies)//valid input
  {
    Company *iParent = NULL;
    Company *jParent = NULL;
    if(companies[i]->parent != NULL)
      {
        //it has a parent, try to find largest parent
        iParent = findLargestParent(companies[i]->parent);
      }
      else
      {
        //it is already the largest parent
        //can't be in same company as j return false
        delete iParent;
        delete jParent;
        return false;
      }
    if(companies[j]->parent != NULL)
      {
        //it has a parent, try to find the largest parent
        jParent = findLargestParent(companies[j]->parent);
      }
      else
      {
        //it is already the largest parent
        //can't be in same company as i return false
        delete iParent;
        delete jParent;
        return false;
      }

    //Now compare parent pointers to see if they point to same parent
    if(jParent == iParent)
    {
      //belong to the same company return true
      delete iParent;
      delete jParent;
      return true
    }
    else
    {
      //not in same company return false
      delete iParent;
      delete jParent;
      return false;
    }
  }
  else
  {
    //invalid input
    cout << "Error: Wrong input." '\n';
    return false;
  }
}

 Company findLargestParent(Company *child)//recursively find the largest parent
{
  if(child->parent == NULL)
  {
    //reached max parent
    return child;
  }
  else
  {
    //keep recursively calling to find the parent
    findLargestParent(child->parent);
  }
}
