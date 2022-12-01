using MoodleFetchBotAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static MoodleFetchBotAPI.Models.Course;

namespace MoodleFetchBotAPI.Services
{
    public class MoodleService
    {
        public List<Course> FetchCourses(string website, string token, Classification classification)
        {
            var client = new RestClient($"{website}/webservice/rest/server.php?wstoken={token}&wsfunction=core_course_get_enrolled_courses_by_timeline_classification&moodlewsrestformat=json&classification={classification}");
            var request = new RestSharp.RestRequest("", Method.Get);
            RestSharp.RestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<Courses>(response.Content).courses;
        }

        public List<CourseDetails> FetchCourseData(string website, string token, int courseId)
        {
            var client = new RestClient($"{website}/webservice/rest/server.php?wstoken={token}&wsfunction=core_course_get_contents&moodlewsrestformat=json&courseid={courseId}");
            var request = new RestSharp.RestRequest("", Method.Get);
            RestSharp.RestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<CourseDetails>>(response.Content);
        }

        public List<CourseAssignInfo> FetchAssignments(string website, string token, int courseId)
        {
            var client = new RestClient($"{website}/webservice/rest/server.php?wstoken={token}&wsfunction=mod_assign_get_assignments&moodlewsrestformat=json&courseids[0]={courseId}");
            var request = new RestSharp.RestRequest("", Method.Get);
            RestSharp.RestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<CourseAssign>(response.Content).courses;
        }

        public List<Discussions> FetchForum(string website, string token, int forumId)
        {
            var client = new RestClient($"{website}/webservice/rest/server.php?wstoken={token}&wsfunction=mod_forum_get_forum_discussions_paginated&moodlewsrestformat=json&forumid={forumId}");
            var request = new RestSharp.RestRequest("", Method.Get);
            RestSharp.RestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<ForumDisc>(response.Content).discussions;
        }

        public string GetToken(string website, string username, string password)
        {
            var client = new RestClient($"{website}/login/token.php");
            var request = new RestSharp.RestRequest($"{website}/login/token.php", Method.Post);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("service", "moodle_mobile_app");
            RestSharp.RestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<AuthToken>(response.Content).token;
        }

    }
}
