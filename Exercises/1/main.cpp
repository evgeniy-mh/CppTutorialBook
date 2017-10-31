#include <iostream>
#include <math.h>

using namespace std;

class Point{
private:
    int n;
    double *coordinates;

public:
    Point(int n){
        this->n=n;
        coordinates=new double[n];
    }

    ~Point(){
        delete []coordinates;
    }

    void EnterCoordinates(){
        for(int i=0;i<n;i++){
            cout<<"Enter coord #"<<i<<" :"<<endl;
            cin>>coordinates[i];
        }
    }

    double FindDistance(){
        double dist=0;

        for(int i=0;i<n;i++){
            dist=dist+coordinates[i]*coordinates[i];
        }
        return sqrt(dist);
    }
};


int main()
{
    cout<<"enter n"<<endl;
    int n;
    cin>>n;

    Point p=Point(n);
    p.EnterCoordinates();

    cout<<"Answer:"<<p.FindDistance()<<endl;
    //cout<<"Answer:"<<5<<endl;

    return 0;
}




