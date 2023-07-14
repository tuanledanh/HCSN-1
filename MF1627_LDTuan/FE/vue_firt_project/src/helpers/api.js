import axios from "axios";

const baseURL = "https://localhost:7033/api/v1/Assets";
const baseCustomerApi = axios.create({
  baseURL: baseURL,
});
const MSIAApi = {
  Customer: {
    Filter: (pageSize, pageNumber, searchText) =>
      baseCustomerApi.get("/Filter", {
        params: {
          pageSize,
          pageNumber,
          searchText,
        },
      }),
    //NewCustomerCode: () => baseCustomerApi.get("/NewCustomerCode"),
    GetAll: () => baseCustomerApi.get(""),
    Create: (customerData) => baseCustomerApi.post("", customerData),
    GetById: (code) => baseCustomerApi.get(`/${code}`),
    Update: (id, customerData) => baseCustomerApi.put(`/${id}`, customerData),
    Delete: (id) => baseCustomerApi.delete(`/${id}`),
  },
};

export default MSIAApi;
