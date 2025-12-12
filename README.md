# :bank: Municipal Service Website: #

This project is an ASP.NET MVC web application that allows community members to engage with their municipality. It provides features for reporting issues, viewing events and announcements, and checking the status of service requests. The website also includes a feedback survey to gather user opinions and improve engagement.

All application data is stored securely in Azure Storage Account (Tables and Blobs).

## :memo: Features ##

1. Report Issues:
Users can log issues (e.g., potholes, broken streetlights, waste collection problems) directly to the municipality. Each issue includes location, category, description, and optional media attachments.

2. Local Events and Announcements: Users are able to browse, search (Based off of Event Name/Title), and filter (Based off Category, Location, and/or date range) through, as well as create Events and Announcements.

3. Service Request Status: Users can view service requests in a hierarchical tree structure. Each request displays its status and progress. Admins can update the status of individual requests, and the system automatically tracks the progress and organizes requests with numbering for clarity.

4. Feedback Survey:
A survey that allows users to provide feedback on their experience with the website to improve municipal services and digital accessibility.

## :clipboard: Setup Instructions: ##

1. Clone or Download the Project:
clone repo: git clone https://github.com/your-repo-link/municipal-service-website.git

2. Open the Project in Visual Studio:
Open the .sln file inside Visual Studio.

3. Run the Application:
Launch the application in your default browser.

## :computer: Navigating through the Website ##

### Homepage: ###
- The homepage introduces the municipal portal providing a bit about the website, and provides quick access to all features.

### Report Issues: ###

- Navigate to Report Issues.

- Fill in the fields for the location, category, description of the issue.

- Optionally upload an image or file.

- Submit the issue, which will be stored in Azure Table Storage and Blob Storage.

- Once submitted you will get redirected to the Home Page and a Success Message will popup.

- Data Structures Used: Linked List, Table, Blob Storage, List for displaying issues.

### Local Events & Announcements: ###

- Navigate to Local Events.

- Use the tabs at the top to switch between Events and Announcements.

- Search and Filter:

  - Enter keywords in the search box to find specific events or announcements.

  - Use the dropdowns to filter by Category, Location, or Date Range.

  - Click Search / Filter to apply the filters.

  - Click the Refresh button to reset all filters and reload the list.
 
- Viewing Events or Announcements:

  - Scroll through the list of events or announcements displayed on the right.

  - For Events, you can see the title, category, location, start and end dates, description, date posted and views.

  - For Announcements, you can see the title, category, location, message, and date posted.

  - Click on an event card to view more details. This will also increase the event’s popularity/view count automatically.
 
- Creating New Event or Announcement:

  - Click Create New Event or Create New Announcement at the top-right.

  - Fill in all required fields for the events/announcements form.

  - For events, select both start and end dates.

  - Click Post Event / Post Announcement to save it.

  - Once submitted, you will be redirected back to the list with a Success Message confirming your submission.
 
- Additional Notes:

  - All required fields must be filled before submission.

  - Uploaded images will be previewed before posting.
 
  - If a user views an event a recommendations filter will appear allowing users to view similar events based on their previous searched/browsed events.

  - Events have a priority/views system, clicking on an event card increases its priority/view and moves that event up the list as more important.
 
  - Data Structures Used: Queues for storing and displaying events and announcements, Dictionaries for searching, Sets for filtering by category, location, or date.
    - Queues: Used to store and display events and announcements in the order they were added, ensuring the newest or most important items appear correctly.
    - Dictionaries: Enables quick searches by event or announcement title, category, or other key characteristics.
    - Sets: Allows efficient filtering by category, location, or date and prevents duplicate entries.
    - In-Memory Updates: Event views, priority, and recommendations are updated automatically in memory, so the website responds quickly to user interactions.

### Service Request Status: ###
- Navigate to Service Request Status.

- View all service requests in a hierarchical tree/graph structure, showing relationships between requests and sub-requests.

- Each request shows its current status and progress with numbered tracking.

- Search and Filter:

  - Use the search box to find requests by ID, category, or location.

  - Apply filters by Status, Location, or Date to narrow down requests.

  - Use the Organize dropdown to sort requests by Status, Location, or Date.

  - Click the Refresh button to reset filters and reload the list.

- Admins can update individual request statuses directly, by accessing the review form and updating its status from within the requests overview.

- The system organizes requests clearly and visually indicates progress using the graph structure for easy navigation and tracking.

- Data Structures Used: Tree for displaying, and searching service requests, graphs for filtering, and grouping/organizing requests (by status/location/date).
  - Tree: Each service request is stored as a node within a tree, the request statuses act as parent nodes, and the service requests are child nodes. This structure keeps the data organized in levels, making it easy to view relationships between requests and monitor their progress.
  - Graph: The graph is used to link requests that share similar details, such as status, location, or date. Each request is represented as a node, and the connections between them show related attributes/characteristics. This allows faster searching and filtering since related requests can be accessed directly without checking each one individually.

### Feedback Survey: ###

- Navigate to Feedback Survey.

- Complete the short form with your feedback.

- Submit to store your response in Azure Table Storage.

- Recieve confirmation message for successfully sending your feedback.

- Data Structures Used: Linked List, Table, List for displaying feedback.

### 📃 Feedback Addressed: ###
#### Part 1: ####
- Improved GUI to be more visually appealing and user-friendly for users.

#### Part 2: ####
- Modified recommendations for events: Users will now be able to select their top 3 most viewed/searched event categories.


## :electric_plug: GitHub Repo: ##
Link: https://github.com/VCNMB-3rd-years/vcnmb-prog7312-2025-poe-ST10284732.git

## :movie_camera: Youtube Videos: ##
### :one: Part 1: ###
Link: https://youtu.be/CK4aN_7uZgo

### :two: Part 2: ###
Link: https://youtu.be/nD-2d3GZgOU

### :three: Part 3: ###
Link: https://youtu.be/GNtcsige_7U
