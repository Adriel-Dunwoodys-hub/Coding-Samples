#include <string>
#include <algorithm>


void permutations(std::string in)
{
	int start = 0;
	permutation(in,start,in.length()-1);
}


void permutation(string in,int pos,int len)
{
    if (pos == len)//base case
    {
        cout << in << "\n";
    }
    else
    {
        for (int curr = pos; curr < in.length(); curr++)
        {
            swap(in[pos],in[curr]);
            permutation(in,pos+1,len);
            swap(in[pos],in[curr]);
        }  
    }
}
