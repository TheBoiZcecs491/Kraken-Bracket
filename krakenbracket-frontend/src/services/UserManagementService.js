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
    singleCreateUser(user, accountType){
        return apiClient.post(`/SingleCreate/${accountType}`, {
            systemID: user.systemID,
            password: user.password,
            accountType: user.accountType
        });
    },
    singleDeleteUser(user, accountType){
        return apiClient.delete(`/SingleDelete/${accountType}`, {
            systemID: user.systemID,
            password: user.password,
            accountType: user.accountType
        });
    },
    singleUpdateUser(user, accountType){
        return apiClient.put(`/SingleUpdate/${accountType}`, {
            systemID: user.systemID,
            firstName: user.firstName,
            lastName: user.lastName,
            email: user.email,
            accountType: user.updateAccountType,
            accountStatus: user.updateAccountStatus,
        });
    }
};
