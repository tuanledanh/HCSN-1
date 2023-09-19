const useDeepObjectEqual = (object1: Object, object2: Object): boolean => {
    const keys1 = Object.keys(object1)
    const keys2 = Object.keys(object2)

    if (keys1.length !== keys2.length) {
        return false
    }

    for (const key of keys1) {
        const val1 = object1[key as keyof Object]
        const val2 = object2[key as keyof Object]
        const areObjects = isObject(val1) && isObject(val2)

        if ((areObjects && !useDeepObjectEqual(val1, val2)) || (!areObjects && val1 !== val2)) {
            return false
        }
    }

    return true
}

const isObject = (object: Object): boolean => object != null && typeof object === 'object'

export default useDeepObjectEqual
