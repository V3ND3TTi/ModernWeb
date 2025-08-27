var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

#region Configure the HTTP pipeline and routes

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/env", () => $"Environment is {app.Environment.EnvironmentName}.");

app.MapGet("/data", () => Results.Json(new
{
    firstName = "John",
    lastName = "Doe",
    age = 30
}));

app.MapGet("/welcome", () => Results.Content(
    content: $"""
    <!doctype html>
    <html lang="en">
    <head>
        <title>Welcome to Northwind Web!</title>
    </head>
    <body>
        <h1>Welcome to Northwind Web!</h1>
    </body>
    </html>
    """,
    contentType: "text/html"));

#endregion

// Start the web server, host the website, and wait for requests.
app.Run(); // this is a thread-blocking call.
WriteLine("This executes after the web server has stopped!");
