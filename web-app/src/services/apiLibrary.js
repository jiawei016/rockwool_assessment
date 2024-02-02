
import fileData from '../index.setting.json'

export const ApiEndPoint = { 
    ApiNews: fileData.env.current.apiEndPoint+'News/Search?newsTitle=0###' 
};