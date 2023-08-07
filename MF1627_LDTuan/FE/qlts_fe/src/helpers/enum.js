const MISAEnum = {
  FORM_MODE: {
    ADD: 1,
    UPDATE: 2,
    VIEW: 3,
    CLONE: 4,
  },
  GENDER: {
    MALE: 0,
    FEMALE: 1,
  },
  HttpStatusCode: {
    Success: 200,
    CreatedSuccess: 201,
    NoContent: 204,
    BadRequest: 400,
    NoAuthen: 401,
    ServerError: 500,
  },
  KEYCODE:{
    DOWN: 40,
    UP: 38,
    ENTER: 13,
  },
};

export default MISAEnum;
