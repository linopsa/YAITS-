using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YAITS_.Models
{
    public class Person
    {
        public string firstName;
        public string lastName;
        public Person(string fn, string ln)
        {
            firstName = fn;
            lastName = ln;
        }
    }
    
    public class Issues
    {
        string shortDescription;
        string detailedDescription;
        int priority;
        bool status;
        Person assignedTo;
        
        public Issues(string _shortDescription, string _detailedDescription, int _priority, bool _status, string firstName, string lastName)
        {
            assignedTo = new Person(firstName, lastName);
            shortDescription = _shortDescription;
            detailedDescription = _detailedDescription;
            priority = _priority;
            status = _status;
        }
        public string issueToString()
        {
            string issue = new string(shortDescription + ";" + detailedDescription + ";" + priority + ";" + status + ";" + assignedTo.firstName + ";" +assignedTo.lastName);
            return issue;
        }

        public string GetShortDescription()
        {
            return shortDescription;
        }
    }
}
