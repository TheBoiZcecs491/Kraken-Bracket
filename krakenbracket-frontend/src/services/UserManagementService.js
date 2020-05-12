import axios from "axios";

const apiClient = axios.create({
  baseURL: `https://localhost:44352/api/UserManagement`,
  withCredentials: false,
  headers: {
    Accept: "application/json",

    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "*"
  }
});

export default {
    singleCreateUser(user){
        console.log(user);
        return apiClient.post(`/SingleCreate`, {
            systemID: user.systemID,
            password: user.password,
            accountType: user.accountType
        });
    }
};
