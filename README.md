# Talk To Your Functions

## How to build chatbots with DialogFlow, Azure Functions, and F#

---

This repository is related to a presentation I gave at the 2018 [Prairie Dev Conn](https://www.prairiedevconn/) in Winnipeg, MB, Canada.

The slides can be found at <https://slides.com/mikesigs/talk-to-your-functions/>

Here you will find both the source code for an Azure Function App, as well as the exported zip file of the related DialogFlow agent.

---

### Prerequisites

You will need to have the following installed to run this code:

- .NET Core 2.0
- Azure Functions Core Tools (beta)
- F#
- Visual Studio Code
- ngrok

---

## Breakdown

### Azure Function App

 In the `src` folder you will find the the Azure Function App. It is written in F#, and acts as the fulfillment logic for a DialogFlow agent.

### DialogFlow Agent

 You can create the DialogFlow agent for yourself by importing `DialogFlowAgent.zip` using the DialogFlow Console.

1. Log into [DialogFlow](https://www.dialogflow.com/)
2. Create a new agent called **BirthdayGreeting**
3. Click on **Export and Import**
4. Click on **Import From Zip** and follow the instructions

---

## Run the app

1. Run the Function App locally by pressing F5
2. Run `ngrok http 7071` (alternatively you could just publish the Function App to Azure)
3. Configure the fulfillment URL in DialogFlow to use your ngrok https URL (or deployed Azure URL), followed by the function route `/api/BirthdayGreeting`
4. Use the Google Assistant test console in DialogFlow to test the app
