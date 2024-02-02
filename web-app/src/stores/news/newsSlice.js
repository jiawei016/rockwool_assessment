import { createSlice } from "@reduxjs/toolkit";

const newsSlice = createSlice({
    name: "news",
    initialState: {
        newsData: {
            "status": "",
            "totalResults": 0,
            "results": []
        },
    },
    reducers: {
        reduxAdd(state, action){
            console.log(action);
            return {...state, newsData: action.payload};
        }
    }
});

export const { reduxAdd } = newsSlice.actions;
export const newsReducer = newsSlice.reducer;