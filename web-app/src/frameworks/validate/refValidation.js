export const RefValidation = (reference) => {
    if(reference.current === undefined){
        return false;
    }
    else if(reference.current.value.trim() === ""){
        return false
    }

    return true;
};