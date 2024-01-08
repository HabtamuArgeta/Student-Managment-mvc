using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagmrnt.Models;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

public class StudentViewModelController : Controller
{
    Uri baseAddress = new Uri("https://localhost:44395/api");
    HttpClient client;

    public StudentViewModelController()
    {
        client = new HttpClient();
        client.BaseAddress = baseAddress;
    }

    public IActionResult Index()
    {
        List<StudentViewModel> studentList = new List<StudentViewModel>();

        HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Students").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            studentList = JsonConvert.DeserializeObject<List<StudentViewModel>>(data);
        }

        return View(studentList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(StudentViewModel student)
    {
        try
        {

            using MultipartFormDataContent multiPartContent = new MultipartFormDataContent();
            multiPartContent.Add(new StringContent(student.Name ?? "", Encoding.UTF8), "name");
            multiPartContent.Add(new StringContent(student.Gender ?? "", Encoding.UTF8), "Gender");
            multiPartContent.Add(new StringContent(student.IsGraduated.ToString() ?? "", Encoding.UTF8), "IsGraduated");

            // Assuming student.Age is an int
            multiPartContent.Add(new StringContent(student.Age.ToString() ?? "", Encoding.UTF8), "Age");

            var imageContent = new StreamContent(student.Photo.OpenReadStream());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(MediaTypeNames.Image.Jpeg);
            multiPartContent.Add(imageContent, "Photo", student.Photo.FileName);

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Students", multiPartContent).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Student created successfully";
                return RedirectToAction("Index");
            }
            return View(student);
        }
        catch(Exception ex)

        {
            TempData["errorMassage"] = ex.Message;
          return View(student);
        }          
    }

    public IActionResult Edit(string id)
    {
        try
        {
            StudentViewModel student = new StudentViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/Students/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<StudentViewModel> (data);
               
            }
          return View(student);
        }
        catch(Exception ex)
        {
            TempData["errorMassage"] = ex.Message;
            return View();

        }
       
    }

    [HttpPost]
    public IActionResult Edit(StudentViewModel student)
    {
        try
        {
            using MultipartFormDataContent multiPartContent = new MultipartFormDataContent();
            multiPartContent.Add(new StringContent(student.Id ?? "", Encoding.UTF8), "id");
            multiPartContent.Add(new StringContent(student.Name ?? "", Encoding.UTF8), "name");
            multiPartContent.Add(new StringContent(student.Gender ?? "", Encoding.UTF8), "Gender");
            multiPartContent.Add(new StringContent(student.IsGraduated.ToString() ?? "", Encoding.UTF8), "IsGraduated");
            multiPartContent.Add(new StringContent(student.Age.ToString() ?? "", Encoding.UTF8), "Age");

            var imageContent = new StreamContent(student.Photo.OpenReadStream());
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(MediaTypeNames.Image.Jpeg);
            multiPartContent.Add(imageContent, "Photo", student.Photo.FileName);

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + $"/Students/{student.Id}", multiPartContent).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Student Updated successfully";
                return RedirectToAction("Index");
            }

            return View(student);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(student);
        }
    }


    [HttpGet]
    public IActionResult Details(string id)
    {
        try
        {
            StudentViewModel student = new StudentViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/Students/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<StudentViewModel>(data);

            }
            return View(student);
        }
        catch (Exception ex)
        {
            TempData["errorMassage"] = ex.Message;
            return View();

        }

    }


    [HttpGet]
    public IActionResult Delete(string id)
    {
        try
        {
            StudentViewModel student = new StudentViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + $"/Students/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<StudentViewModel>(data);

            }
            return View(student);
        }
        catch (Exception ex)
        {
            TempData["errorMassage"] = ex.Message;
            return View();

        }

    }


    [HttpPost]
    public IActionResult DeleteConfirmed(string id)
    {
        try
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + $"/Students/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Student deleted successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        catch(Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View();
        }
       
    }

}

