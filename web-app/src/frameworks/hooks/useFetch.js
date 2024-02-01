import {Observable} from 'rxjs'

export default class UseFetch {
  FetchGet = (urlEndpoint, params) => {
    if(params !== null && params !== undefined){
      for(let i=0; i < params.length; i++){
        let toReplace = i + "###";
        urlEndpoint = urlEndpoint.replace(toReplace, params[i]);
      }
    }
    try {
      const requestOptions = {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      };
      return new Observable(observer => {
        const _promise = new Promise(resolve => {
          fetch(urlEndpoint, requestOptions)
          .then(response => {
            const jsonData = response.json();
            console.log("FetchGet");
            console.log(jsonData);
            resolve(jsonData);
          });
        });
        _promise.then(data => observer.next(data));
      });

    } catch (error) {
    } finally {
    }
  };
}
