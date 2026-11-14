# RaceDay - PROG6212 POE Part 3
ASP.NET Core MVC - Race Event Management

## 1. What does this app do?
RaceDay is a race event web app. Users can register, log in, view races, and book races.
Organisers can create events, add categories, manage enrolments, enter results and upload a banner image for each event.

## 2. Projects
- `RaceDay.API` - the API project
- `RaceDay.Mvc` - the web project (startup project)

## 3. MVC Structure
**Model - The Data**
- `EventVM` - Id, Name, Date, Location, BannerImage
- `CategoryVM` - Id, Name, Type, Price
- `EnrolmentVM` - Name, CategoryName, CategoryId

**View - What the user sees**
- Folder: `Views/Home/`
- Pages: Dashboard, Events, ManageEvents, CreateEvent, EditEvent, Categories, ManageCategories, Enrol, Enrolments, Profile2, MyResults, RaceResults, ResultsEntry
- Banner is shown with: `<img src="@e.BannerImage">`

**Controller - The Brain**
- File: `Controllers/HomeController.cs`
- Actions: CreateEvent, EditEvent, DeleteEvent, ManageEvents, Events, Dashboard, Enrol, Enrolments, ResultsEntry, Profile, MyResults, RaceResults

## 4. Blob Storage Integration
Banner images are NOT saved on the laptop. They are saved in Azure Blob Storage.

- Package: `Azure.Storage.Blobs`
- Service: `Services/BlobService.cs`
- Container name: `banners`
- How it works:
    1. Organiser uploads banner in CreateEvent / EditEvent
    2. File is uploaded to Azure Blob Storage
    3. Azure returns a URL like `https://racedaystorage.blob.core.windows.net/banners/xxx.jpg`
    4. That URL is saved in `EventVM.BannerImage`
    5. The View shows the image from that URL, so it works on any computer

## 5. How to Run WITHOUT Docker
1. Open RaceDay in Visual Studio.
2. Set RaceDay.Mvc as the startup project.
3. Press F5 to run.
4. Register a new account, then log in.
Or in terminal:
```bash
dotnet restore
dotnet run
