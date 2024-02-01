import { createSlice } from "@reduxjs/toolkit";

const newsSlice = createSlice({
    name: "news",
    initialState: {
        newsList: [],
    },
    reducers: {
        add(state, action){
            const updatedTaskList = state.taskList.concat(action.payload);
            return {...state, taskList: updatedTaskList};
        },
        remove(state, action){
            const updatedTaskList = state.taskList.filter(item => item.TaskName !== action.payload.TaskName);
            return {...state, taskList: updatedTaskList};
        }
    }
});

export const { add, remove } = newsSlice.actions;
export const newsReducer = newsSlice.reducer;