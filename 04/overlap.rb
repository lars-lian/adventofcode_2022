star_one = 0
star_two = 0

File.readlines('inputdata/input').each do |line|
    zone_pair = line.split(",")
    l_range = zone_pair[0].split("-")
    r_range = zone_pair[1].split("-")

    l_low = Integer(l_range[0])
    l_high = Integer(l_range[1])
    r_low = Integer(r_range[0])
    r_high = Integer(r_range[1])

    if (l_low <= r_low) && (l_high >= r_high) || (l_low >= r_low) && (l_high <= r_high) then
        star_one += 1
    end

    if (l_low <= r_high) && (l_high >= r_low) || (l_low >= r_high) && (l_high <= r_low) then
        star_two += 1
    end
end

puts "star one: %d" % star_one
puts "star two: %d" % star_two
