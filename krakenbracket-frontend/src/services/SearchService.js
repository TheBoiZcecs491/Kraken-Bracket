import axios from "axios";

const apiClient = axios.create({
  baseURL: `https://localhost:5001`,
  withCredentials: false,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "*"
  }
});

export default {
    getSearchBrackets(search) {   //, pageNum, skipPage) {
        return apiClient.get("api/search/" + search);
    }
};