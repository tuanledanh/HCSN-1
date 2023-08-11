export default {
    HttpStatusCode: {
        OK: 200,
        CREATED: 201,
        ACCEPTED: 202,
        NO_CONTENT: 204,
        BAD_REQUEST: 400,
        UNAUTHORIZED: 401,
        NOT_FOUND: 404,
        INTERNAL_SERVER_ERROR: 500
    },
    FROM_MODE: {
        create: 'create',
        update: 'update',
        replicate: 'replicate'
    },
    TOAST_MODE: {
        export: 'export',
        delete: 'delete'
    }
}
