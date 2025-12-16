pub trait BaseError {
    fn get_message(&self) -> &str;
    fn get_code(&self) -> u16;
    fn set_message(&mut self, msg: String);
    fn set_code(&mut self, code: u16);
    fn new(msg: String, code: u16) -> Self;
}

pub const CREATE_FILE_ERROR_CODE: u16 = 1001;
pub const READ_FILE_ERROR_CODE: u16 = 1002;
pub const CREATE_DIR_ERROR_CODE: u16 = 1003;
pub const READ_DIR_ERROR_CODE: u16 = 1004;

pub struct Error {
    msg: String,
    code: u16,
}

impl BaseError for Error {
    fn get_message(&self) -> &str {
        &self.msg
    }

    fn get_code(&self) -> u16 {
        self.code
    }

    fn set_message(&mut self, msg: String) {
        self.msg = msg;
    }

    fn set_code(&mut self, code: u16) {
        self.code = code;
    }

    fn new(msg: String, code: u16) -> Self {
        Error { msg, code }
    }
}
