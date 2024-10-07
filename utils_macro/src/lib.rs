extern crate proc_macro;
use proc_macro::TokenStream;
use quote::quote;
use syn::{parse_macro_input, ItemStruct};

#[proc_macro_attribute]
pub fn entity(attr: TokenStream, item: TokenStream) -> TokenStream {
    let input = parse_macro_input!(item as ItemStruct);
    let name = &input.ident;

    let expanded = quote! {
      pub struct #name {
        // custom attributes
        name: String
      }

      impl #name {
        // implement custom methods
        pub fn new<T: Into<String>>(name: T) -> Self {
          Self { name: name.into() }
        }

        pub fn get_name(&self) -> &str {
          &self.name
        }
      }
    };

    TokenStream::from(expanded)
}
