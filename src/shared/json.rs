use serde::{Deserialize, Serialize};
use std::fs;

pub trait JSON
where
    Self: Serialize + for<'de> Deserialize<'de>,
{
    fn to_string(&self) -> String {
        serde_json::to_string_pretty(self).unwrap()
    }

    fn from_file(path: &str) -> Result<Self, ()> {
        if let Ok(file) = fs::read(path) {
            if let Ok(content) = String::from_utf8(file) {
                return Ok(Self::from_string(&content));
            }
        }

        Err(())
    }

    fn from_string(data: &str) -> Self {
        serde_json::from_str(data).unwrap()
    }
}
