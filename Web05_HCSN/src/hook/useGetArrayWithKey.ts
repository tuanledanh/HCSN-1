const useGetArrayWithKey = (array: object[], key: string, id: string): Array<object> => {
    return array.map((item: object) => ({
        [id]: item[id as keyof object],
        [key]: item[key as keyof object]
    }))
}

export default useGetArrayWithKey
