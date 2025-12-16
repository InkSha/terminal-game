use std::fs;

use crate::shared::error::{
    BaseError, Error, CREATE_DIR_ERROR_CODE, CREATE_FILE_ERROR_CODE, READ_DIR_ERROR_CODE,
    READ_FILE_ERROR_CODE,
};

pub fn create_file<T>(filepath: T, content: T) -> Result<bool, Error>
where
    T: Into<String>,
{
    let filepath = filepath.into();
    let content = content.into();
    match std::fs::write(filepath, content) {
        Ok(_) => Ok(true),
        Err(e) => Err(Error::new(e.to_string(), CREATE_FILE_ERROR_CODE)),
    }
}

pub fn read_file<T>(filepath: T) -> Result<String, Error>
where
    T: Into<String>,
{
    let filepath = filepath.into();
    match std::fs::read_to_string(filepath) {
        Ok(content) => Ok(content),
        Err(e) => Err(Error::new(e.to_string(), READ_FILE_ERROR_CODE)),
    }
}

pub fn create_dir<T>(dirpath: T) -> Result<bool, Error>
where
    T: Into<String>,
{
    let dirpath = dirpath.into();
    match fs::create_dir_all(dirpath) {
        Ok(_) => Ok(true),
        Err(e) => Err(Error::new(e.to_string(), CREATE_DIR_ERROR_CODE)),
    }
}
pub fn read_dir<T>(dirpath: T) -> Result<Vec<String>, Error>
where
    T: Into<String>,
{
    let dirpath = dirpath.into();
    let mut entries = Vec::new();
    match fs::read_dir(dirpath) {
        Ok(read_dir) => {
            for entry in read_dir {
                match entry {
                    Ok(e) => {
                        if let Some(path_str) = e.path().to_str() {
                            entries.push(path_str.to_string());
                        }
                    }
                    Err(e) => {
                        return Err(Error::new(e.to_string(), READ_DIR_ERROR_CODE));
                    }
                }
            }
            Ok(entries)
        }
        Err(e) => Err(Error::new(e.to_string(), READ_DIR_ERROR_CODE)),
    }
}
