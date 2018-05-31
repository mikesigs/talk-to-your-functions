namespace TalkToYourFunctions

open Newtonsoft.Json.Serialization
module DialogFlow =

    type QueryParameters = {
        ``given-name``: string
        ``last-name``: string
        birthdate: System.DateTime
    }

    type QueryResult = {
        parameters: QueryParameters
    }

    type DialogFlowRequest = {
        queryResult: QueryResult
    }


    type SimpleResponse = {
        textToSpeech: string
    }

    type RichResponseItem = {
        simpleResponse: SimpleResponse
    }

    type RichResponse = {
        items: RichResponseItem list
    }

    type GooglePayload = {
        richResponse: RichResponse
    }

    type Payload = {
        google: GooglePayload
    }

    type DialogFlowResponse = {
        payload: Payload
    }