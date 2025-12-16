pub fn output<T>(msg: T)
where
    T: Into<String>,
{
    let msg = msg.into();
    println!("{}", msg);
}
