<p align="center">
  <img src="https://img.icons8.com/fluency/100/commercial-development-management.png" alt="Employee Manager Logo" width="120" height="120">
</p>

<h1 align="center">Employee Manager ASP.NET Application</h1>

<h2>Project Overview</h2>
<p>Employee Manager is a web application developed using ASP.NET and C#. The project incorporates MS SQL Server via Entity Framework and is structured with microservices and API management using Ocelot. It also integrates features such as password hashing (SHA256), validation, cookie-based authentication, and all in an asynchronous process. With CRUD functionality for employees under the admin role, it provides managers the ability to manage employees, while employees without admin roles can view their own data.</p>

<h2>Project Details</h2>
<ul>
  <li><strong>Languages:</strong> 
    <a href="https://learn.microsoft.com/en-us/dotnet/csharp/" target="_blank">
      <img src="https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white" alt="C#">
    </a>
  </li>
  <li><strong>Technologies:</strong> 
    <a href="https://dotnet.microsoft.com/" target="_blank">
      <img src="https://img.shields.io/badge/.NET%20Framework-512BD4?style=flat&logo=.net&logoColor=white" alt=".NET Framework">
    </a>
    <a href="https://learn.microsoft.com/en-us/ef/" target="_blank">
      <img src="https://img.shields.io/badge/Entity%20Framework-512BD4?style=flat&logo=.net&logoColor=white" alt="Entity Framework">
    </a>
    <a href="https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html" target="_blank">
      <img src="https://img.shields.io/badge/Ocelot%20API-6A4A3C?style=flat&logo=api&logoColor=white" alt="Ocelot API">
    </a>
  </li>
  <li><strong>Security:</strong> 
    <a href="https://en.wikipedia.org/wiki/SHA-2" target="_blank">
      <img src="https://img.shields.io/badge/SHA256-282C34?style=flat&logo=lock&logoColor=white" alt="SHA256">
    </a>
    <a href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Cookies" target="_blank">
      <img src="https://img.shields.io/badge/Cookie%20Auth-4CAF50?style=flat&logo=lock&logoColor=white" alt="Cookie Authentication">
    </a>
  </li>
  <li><strong>Project Type:</strong> 
    <a href="https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0" target="_blank">
      <img src="https://img.shields.io/badge/ASP.NET%20Web%20App-0078D6?style=flat&logo=asp.net&logoColor=white" alt="ASP.NET Web App">
    </a>
  </li>
</ul>

<h2>Development Details</h2>
<p>This project was created as part of a homework assignment at IT Step Computer Academy. It demonstrates CRUD operations for employee management with role-based access control, including admin privileges and employee self-service, integrating MS SQL Server with Entity Framework. The project also uses Bootstrap for UI styling and Swagger for API documentation.</p>

<h2>Getting Started</h2>
<p><strong>Note:</strong> This project requires an MS SQL Server setup with proper connection strings.</p>

<p>Follow these steps to set up the project:</p>
<ol>
  <li>Clone the repository: 
    <pre><code>git clone https://github.com/zabavb/Employee-manager-asp-app.git</code></pre>
  </li>
  <li>Configure your MS SQL Server and update the connection strings in the <code>appsettings.json</code> file.</li>
  <li>Install the required NuGet packages, including <strong>Ocelot</strong>, <strong>Microsoft.EntityFrameworkCore</strong>, <strong>Swashbuckle.AspNetCore</strong>, and others as needed.</li>
  <li>Open the solution file in Visual Studio, build the project, and run the application.</li>
</ol>

<h2>Features</h2>
<ul>
  <li><strong>Role-based Access:</strong> Admin users can perform CRUD operations, while employees can only view their personal data.</li>
  <li><strong>Security:</strong> SHA256 hashing for passwords, cookie-based authentication, and an authorization system.</li>
  <li><strong>API:</strong> Integrated API management using Ocelot, with Swagger documentation.</li>
</ul>

<h2>Usage</h2>
<p>To use this application, ensure you have the necessary permissions and connection strings configured. Admins can manage employees, and employees can view their personal details.</p>

<h2>Contributing</h2>
<p>Contributions are welcome! If you have any suggestions or improvements, feel free to fork the repository and submit a pull request.</p>
<ol>
  <li>Fork the Repository: Click the "Fork" button at the top-right of this page.</li>
  <li>Create a Branch: Create a new branch for your changes.</li>
  <li>Commit Changes: Make your changes and commit them with a descriptive message.</li>
  <li>Push to Your Fork: Push your changes to your forked repository.</li>
  <li>Submit a Pull Request: Go to the "Pull Requests" tab and submit a new pull request.</li>
</ol>

<h2>Contact</h2>
<p>For any questions or inquiries, you can reach me at <a href="mailto:bilonizkavik@agmail.com">email</a> or connect with me on <a href="https://www.linkedin.com/in/viktor-bilonizhka" target="_blank">LinkedIn</a>.</p>

<h2>References</h2>
<ul>
  <li><a href="https://learn.microsoft.com/en-us/dotnet/csharp/" target="_blank">C# Documentation</a></li>
  <li><a href="https://learn.microsoft.com/en-us/ef/" target="_blank">Entity Framework Documentation</a></li>
  <li><a href="https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html" target="_blank">Ocelot API Documentation</a></li>
  <li><a href="https://learn.microsoft.com/en-us/dotnet/architecture/microservices/" target="_blank">Microservices Architecture</a></li>
  <li><a href="https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-7.0" target="_blank">ASP.NET Core Authentication with Cookies</a></li>
  <li><a href="https://swagger.io/docs/" target="_blank">Swagger Documentation</a></li>
  <li><a href="https://getbootstrap.com/" target="_blank">Bootstrap Documentation</a></li>
  <li><a href="https://learn.microsoft.com/en-us/sql/?view=sql-server-ver16" target="_blank">MS SQL Server Documentation</a></li>
  <li><a href="https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256?view=net-7.0" target="_blank">SHA256 Hashing Documentation</a></li>
</ul>


<h2>Acknowledgements</h2>
<ul>
  <li>Thanks to IT Step Academy for providing the resources and guidance for this project.</li>
  <li>Special thanks to Microsoft for their comprehensive documentation and cloud services.</li>
  <li>Gratitude to the open-source community for NuGet packages and contributions.</li>
</ul>

<hr>

<p align="center">Feel free to modify or extend this README to fit your needs better. Happy coding!</p>
