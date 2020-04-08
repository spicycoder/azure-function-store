# Sending data to Azure Event Hubs

> Useful resources

* <https://docs.microsoft.com/en-us/azure/event-hubs/get-started-dotnet-standard-send-v2>

* <https://youtu.be/fnQG47ojccc>

---

# Azure Functions - Persist in MongoDB

1. Create a MongoDB instance, preferably in cloud.

2. Update the connection string in the code

    ```csharp
    // Hard-coded Connection String & Database name, as they are not the point of focus for this exercise
    var connectionString = "<Replace with your connection string>";
    ```

3. Fire up (in debug mode)

4. Post a sample product to
    <http://localhost:7071/api/AddProduct>

    ```json
    {
        "name": "Potato",
        "description": "Organically grown, freshly cultivated potatoes!",
        "price": "15.99"
    }
    ```

    > Replace `7071` with your respective port number

5. Verify in MongoDB instance if your product is added

---
