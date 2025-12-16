use crate::{
    files::files::create_dir,
    shared::{error::BaseError, json::JSON},
};
use serde::{Deserialize, Serialize};
use std::path::Path;

#[derive(Serialize, Deserialize, Debug, Clone)]
pub struct Area {
    pub name: String,
    childrens: Vec<Area>,
}

impl JSON for Area {}

impl Area {
    pub fn new(name: &str) -> Self {
        Area {
            name: name.to_string(),
            childrens: Vec::new(),
        }
    }

    pub fn add_child(&mut self, child: Area) {
        self.childrens.push(child);
    }

    pub fn add_childrens(&mut self, children: Vec<Area>) {
        self.childrens.extend(children);
    }

    pub fn init_area<T>(&self, root: T)
    where
        T: Into<String>,
    {
        self.create(root);
    }

    fn create<T>(&self, parent: T)
    where
        T: Into<String>,
    {
        let parent = parent.into();
        if let Some(p) = Path::new(parent.as_str()).join(&self.name).to_str() {
            if let Err(e) = create_dir(p) {
                eprintln!("Error creating directory {}: {}", p, e.get_message());
            } else {
                for child in &self.childrens {
                    child.create(p);
                }
            }
        }
    }
}
