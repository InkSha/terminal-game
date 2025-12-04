use std::io::{self, Write};

// 地图为文件结构形式
// e.g. 主世界/北部大陆/人类帝国/边疆/边境城镇
// 实体则以文件形式存储
// e.g. /../边境城镇/酒馆.build, /../边境城镇/酒馆/服务员.npc, /../边境城镇/城门.build

fn main() {
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
                println!("退出游戏...");
                break;
            }
            _ => println!("无效命令，请再试一次。"),
        }
    }
}
