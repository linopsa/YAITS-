using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace YAITS_.Models
{
    public class IssuesManager
    {
        List<Issues> issues = new List<Issues>();
        Object thisLock = new Object();

        public IssuesManager()
        {
            lock (thisLock)
            {
                LoadFromDB();
            }
        }

        private void LoadFromDB()
        {
            //the suggested implementation is described in https://hevodata.com/learn/sql-server-rest-api-integration/

            issues.Add(new Issues("problem1", "The first problem", 7, false, "Steve", "Jobs"));
            issues.Add(new Issues("problem2", "The second problem", 1, false, "John", "Snow"));
            issues.Add(new Issues("problem3", "The third problem", 3, true, "Patricia", "Katz"));
            issues.Add(new Issues("problem4", "The forth problem", 5, false, "Dan", "Stepanov"));
            issues.Add(new Issues("problem5", "The fifth problem", 7, true, "Another", "One"));
            issues.Add(new Issues("problem6", "The last loaded problem", 7, false, "", ""));
        }

        public void AddIssue(Issues issue)
        {
            lock (thisLock)
            {
                CheckIfUnique(issue);
                issues.Add(issue);
                SavetoDB(issue);
            }
        }

        public void UpdateIssue(Issues newIssue)
        {
            lock (thisLock)
            {
                for (int count = 0; count < issues.Count; count++)
                {
                    if (issues[count].GetShortDescription().Equals(newIssue.GetShortDescription()))
                    {
                        issues[count] = newIssue;
                        UpdateIssueInDB(issues[count], newIssue);
                        return;
                    }
                }
            }
            var resp = new HttpResponseMessage(HttpStatusCode.NotFound);
            throw new HttpResponseException(resp);
        }

        private void UpdateIssueInDB(Issues issues, Issues newIssue)
        {
            //throw new NotImplementedException();
        }

        private void CheckIfUnique(Issues issue)
        {
            lock (thisLock)
            {
                for (int i = 0; i < issues.Count; i++)
                {
                    if (issue.GetShortDescription().Equals(issues[i].GetShortDescription()))
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.Conflict);
                        throw new HttpResponseException(resp);
                    }
                }
            }
        }

        private void SavetoDB(Issues issue)
        {
            //throw new NotImplementedException();
        }

        public List<Issues> GetIssues()
        {
            lock (thisLock)
            {
                return issues;
            }
        }

        public IEnumerable<string> GetAllIssues()
        {
            string[] retVal = new string[issues.Count];
            lock (thisLock)
            {
                int issuesCount = 0;
                foreach (Models.Issues issue in issues)
                {
                    retVal[issuesCount++] = issue.issueToString();
                }
            }
            return retVal;
        }

        public string GetIssueById(string shortDescription)
        {
            string retVal = string.Empty;
            lock (thisLock)
            {
                foreach (Models.Issues issue in issues)
                {
                    if (issue.GetShortDescription().Equals(shortDescription))
                    {
                        retVal = issue.issueToString();
                        break;
                    }
                }
            }
            return retVal;
        }

        public void DeleteIssue(string shortDescription)
        {
            lock (thisLock)
            {
                for (int i = 0; i < issues.Count; i++)
                {
                    if (shortDescription.Equals(issues[i].GetShortDescription()))
                    {
                        DeleteFromDB(issues[i]);
                        issues.RemoveAt(i);
                        return;
                    }
                }
            }
            var resp = new HttpResponseMessage(HttpStatusCode.NotFound);
            throw new HttpResponseException(resp);
        }

        private void DeleteFromDB(Issues issues)
        {
            //throw new NotImplementedException();
        }
    }
}
