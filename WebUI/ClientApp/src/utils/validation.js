import notify from 'devextreme/ui/notify'

export function getNotifyErrorMessage(reason) {
    notify(getMessages(reason.response), 'error', 1000);
}

export function getMessages(message) {
    // eslint-disable-next-line no-console
    console.log(message)
    return message.data.errors ? flatten(Object.values(message.data.errors)).join(', ') : message.data.title

    function flatten(arr) {
        return arr.reduce(function (flat, toFlatten) {
            return flat.concat(Array.isArray(toFlatten) ? flatten(toFlatten) : toFlatten);
        }, [])
    }
}
