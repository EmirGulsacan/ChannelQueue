# ChannelQueue Web API

This project is a Web API built using ASP.NET Core. The API leverages the Channel structure to enqueue messages and process them with a specified delay.

## Project Structure

The project consists of the following components:

- **Producer**: Adds messages to the channel.
- **Consumer**: Reads and processes messages from the channel.
- **MessageController**: Accepts a list of messages via the API and enqueues them using the Producer.
- **ChannelSettings**: Reads the channel capacity from the `appsettings.json` file.

## Setup

1. Clone the repository:

```bash
git clone https://github.com/EmirGulsacan/ChannelQueue.git
cd ChannelQueue
```
```bash
2.Restore Dependencies
dotnet restore
```
```bash
3. Run The Project
dotnet run
```
Configuration
The channel capacity is read from the appsettings.json file. The default capacity is set to 100. You can adjust this value as needed.

```appsettings.json

"ChannelSettings": {
    "Capacity": 100
}

```
API Usage
The API accepts a list of messages via a POST request and enqueues these messages.

Send Messages
Endpoint: /message

Method: POST

Content Type: application/json

Request Body (Example):

["Hello, World!", "Another message"]

Example cURL Request

curl -X POST "http://localhost:5001/message" -H "Content-Type: application/json" -d '["Hello, World!", "Another message"]'

Project Files
Program.cs: Main entry point and service configuration.
Models/ChannelSettings.cs: Model for channel capacity settings.
Services/Producer.cs: Class responsible for adding messages to the channel.
Services/Consumer.cs: Class responsible for reading and processing messages from the channel.
Controllers/MessageController.cs: Controller defining the API endpoint.
Development and Contribution
To contribute, please open a pull request or create an issue.

License
This project is licensed under the MIT License. See the LICENSE file for more details.
