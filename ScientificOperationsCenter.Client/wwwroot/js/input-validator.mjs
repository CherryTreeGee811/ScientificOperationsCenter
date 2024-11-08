export function validateDate(date) {
    const datePattern = /^[0-9]{4}\-[0-1][0-9]\-[0-3][0-9]$/;

    if (date.match(datePattern)) {
        return true;
    }

    return false;
}


export function validateTimeFrame(timeFrame) {
    switch (timeFrame) {
        case "day":
        case "month":
        case "year":
            return true;
    }
    return false;
}