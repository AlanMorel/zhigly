function lengthErrorMessage(data, min, max) {
    return "Your " + data + " must be " + min + " to " + max + " characters long.";
}

function checkSize(text, min, max) {
    return text.length < min || text.length > max;
}

function validateTitle(text) {
    if (checkSize(text, 10, 100)){
        return lengthErrorMessage("title", 10, 100);
    }
    return "";
}

function validateDescription(text) {
    if (checkSize(text, 25, 1000)){
        return lengthErrorMessage("description", 25, 1000);
    }
    return "";
}

function validateMessage(text) {
    if (checkSize(text, 10, 1000)){
        return lengthErrorMessage("message", 10, 1000);
    }
    return "";
}

function validateName(text) {
    if (checkSize(text, 2, 20)){
        return lengthErrorMessage("name", 2, 20);
    }
    if (!(/^[a-zA-Z\s]+$/.test(text))) {
        return "Your name can only contain letters.";
    }
    return "";
}

function validatePrimaryName(text, organization) {
    if (organization) {
        if (checkSize(text, 2, 20)) {
            return lengthErrorMessage("organization name", 2, 20);
        }
    } else {
        if (checkSize(text, 2, 20)) {
            return lengthErrorMessage("first name", 2, 20);
        }
        if (!(/^[a-zA-Z]+$/.test(text))) {
            return "A first name can only contain letters.";
        }
    }
    return "";
}

function validateSecondaryName(text, organization) {
    if (!organization) {
        if (checkSize(text, 2, 20)) {
            return lengthErrorMessage("last name", 2, 20);
        }
        if (!(/^[a-zA-Z]+$/.test(text))) {
            return "A last name can only contain letters.";
        }
    }
    return "";
}

function validatePassword(text) {
    if (checkSize(text, 6, 20)){
        return lengthErrorMessage("password", 6, 20);
    }
    return "";
}

function validateConfirm(text, password) {
    if (!(text === password)) {
        return "The two passwords do not match.";
    }
    return "";
}

function validateEmail(text) {
    if (checkSize(text, 1, 30)){
        return lengthErrorMessage("email", 1, 30);
    }
    return "";
}