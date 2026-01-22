using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonComparer : IComparer<Person>
{
    private PersonSortMode mode;

    public PersonComparer(PersonSortMode mode)
    {
        this.mode = mode;
    }
    public int Compare(Person x, Person y)
    {
        switch (mode)
        {
            case PersonSortMode.Name:
                return x.Name.CompareTo(y.Name);
            case PersonSortMode.Age:
                return x.Age.CompareTo(y.Age);
            case PersonSortMode.Both:
                int ageCompare = y.Age.CompareTo(x.Age);
                if (ageCompare != 0)
                    return ageCompare;

                return x.Name.CompareTo(y.Name); 
            default:
                return 0;
        }
    }
}
