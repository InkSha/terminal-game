use serde::{Deserialize, Serialize};
use std::{fs, path::Path};

use crate::json::JSON;

#[derive(Serialize, Deserialize, Debug)]
pub struct Area {
    name: String,
    child: Vec<Area>,
}

impl JSON for Area {}
impl Area {
    pub fn new<T: Into<String>>(name: T) -> Self {
        Self {
            name: name.into(),
            child: vec![],
        }
    }

    pub fn push(mut self, area: Vec<Area>) -> Self {
        self.child.extend(area);
        self
    }

    pub fn init(&self) {
        self.create("");
    }

    pub fn create_from_file(path: &str) {
        if let Ok(area) = Area::from_file(path) {
            area.init();
        }
    }

    fn create(&self, parent: &str) {
        if let Some(p) = Path::new(parent).join(&self.name).to_str() {
            fs::create_dir_all(&p).unwrap();

            for index in 0..self.child.len() {
                if let Some(area) = self.child.get(index) {
                    area.create(p);
                }
            }
        }
    }
}
