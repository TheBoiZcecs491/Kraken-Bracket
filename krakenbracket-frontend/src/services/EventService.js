import axios from "axios";

const apiClient = axios.create({
  baseURL: `https://localhost:44352`,
  withCredentials: false,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "*"
  }
});

export default {
  getEvents() {
    return apiClient.get("api/events");
  },
  getEventByID(eventID) {
    return apiClient.get("api/events/" + eventID);
  },
  getEventHost(eventID) {
    return apiClient.get("api/events/GetEventHost/" + eventID);
  },
  getEventInfo(eventID) {
    return apiClient.get("api/events/GetEventInfo/" + eventID);
  },
  getBracketEvent(eventID) {
    return apiClient.get("api/events/GetBracketEvent/" + eventID);
  }
  // checkHost(eventID, hashedUserID){
  //   return apiClient.get("api/events/checkHost");
  // },
  // checkResgistration(eventID, hashedUserID){
  //   return apiClient.get("api/events/checkRegistration");
  // }
};
