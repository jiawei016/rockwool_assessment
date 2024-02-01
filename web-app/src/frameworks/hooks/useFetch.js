export default class UseFetch {
  FetchGet = async (urlEndpoint) => {
    try {
      const requestOptions = {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      };

      const response = await fetch(urlEndpoint, requestOptions);

      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      const jsonData = await response.json();
    } catch (error) {
    } finally {
    }
  };

  FetchPost = async (urlEndpoint, endPointData) => {
    let requestData = null
    if(endPointData != null && endPointData != undefined){
        requestData = JSON.stringify(endPointData);
    }
    try {
      const requestOptions = {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: requestData,
      };

      const response = await fetch(urlEndpoint,requestOptions);

      if (!response.ok) {
        throw new Error("Network response was not ok");
      }

      const jsonData = await response.json();
    } catch (error) {
    } finally {
    }
  };
}
