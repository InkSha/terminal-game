use std::io::{self, Write};

use crate::map::area::Area;

mod files;
mod map;
mod shared;

fn init() -> bool {
    let mut main_land = Area::new("main land");
    let mut white_area = Area::new("white area");
    let mut black_area = Area::new("black area");
    let mut red_area = Area::new("red area");

    white_area.add_childrens(vec![
        Area::new("kingdom"),
        Area::new("empire"),
        Area::new("union"),
    ]);

    black_area.add_childrens(vec![
        Area::new("old forest"),
        Area::new("died plain"),
        Area::new("black sea"),
    ]);

    red_area.add_childrens(vec![
        Area::new("blood mountain"),
        Area::new("snow"),
        Area::new("desert"),
    ]);

    main_land.add_childrens(vec![white_area, black_area, red_area]);

    main_land.init_area("./map");

    return true;
}

fn main() {
    let init_status = init();

    if !init_status {
        println!("初始化失败");
    }

    loop {
        // break;
        print!("> ");
        io::stdout().flush().unwrap(); // 刷新输出以显示提示
        let mut input = String::new();
        io::stdin().read_line(&mut input).expect("无法读取输入");

        let command = input.trim();

        match command {
            "look" => println!("你环顾四周，看到了一片荒野。"),
            "north" => println!("你向北移动了一段距离。"),
            "init" => {}
            "quit" => {
                println!("退出...");
                break;
            }
            _ => println!("无效命令，请再试一次。"),
        }
    }
}
