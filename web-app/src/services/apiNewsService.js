
import {ApiEndPoint} from './apiLibrary'
import UseFetch from '../frameworks/hooks/useFetch';
import { Observable } from 'rxjs';

export const ApiNewsService = (param) => {
    return new Observable(observer => {
        new UseFetch().FetchGet(ApiEndPoint.ApiNews, param).subscribe(data => {
            if(data == null){
                alert("Network Error");
            }
            else{
                observer.next(data);
            }
        });
    })
}

export const ApiNewsSearchByPaginationService = (param) => {
    console.log(param);
    return new Observable(observer => {
        new UseFetch().FetchGet(ApiEndPoint.ApiNewsSearchByPagination, param).subscribe(data => {
            if(data == null){
                alert("Network Error");
            }
            else{
                observer.next(data);
            }
        });
    })
}